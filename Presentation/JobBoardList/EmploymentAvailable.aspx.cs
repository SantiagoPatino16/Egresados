using System;
using System.Linq;
using LogicBusiness.Service;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Attributes;
using DataAccess.ConnectionDB;

namespace Presentation.JobBoardList
{
    public partial class EmploymentAvailable : Page
    {
        private JobOffertsService _jobOffersService = new JobOffertsService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CerrarOfertasVencidas();
                CargarFiltros();
                LoadJobs();
            }
        }

        MainPage MasterPage
        {
            get { return (MainPage)this.Master; }
        }

        // Cerrar automáticamente ofertas cuya FechaCierre ya venció
        private void CerrarOfertasVencidas()
        {
            using (var ctx = new RSContext())
            {
                var vencidas = ctx.OfertasEmpleo
                    .Where(x => x.Estado == AttributesJobOfferts.EstadoOferta.Activa &&
                                x.FechaCierre < DateTime.Now)
                    .ToList();

                foreach (var oferta in vencidas)
                {
                    oferta.Estado = AttributesJobOfferts.EstadoOferta.Cerrada;
                }

                ctx.SaveChanges();
            }
        }

        // Cargar filtros
        private void CargarFiltros()
        {
            ddlCategories.DataSource = _jobOffersService.ListarCategorias();
            ddlCategories.DataTextField = "Categoria";
            ddlCategories.DataValueField = "IdCategoria";
            ddlCategories.DataBind();
            ddlCategories.Items.Insert(0, new ListItem("Todas", "0"));

            ddlWorkModes.DataSource = _jobOffersService.ListarModalidades();
            ddlWorkModes.DataTextField = "Modalidad";
            ddlWorkModes.DataValueField = "IdModalidadTrabajo";
            ddlWorkModes.DataBind();
            ddlWorkModes.Items.Insert(0, new ListItem("Todas", "0"));

            ddlContractTypes.DataSource = _jobOffersService.ListarTiposContrato();
            ddlContractTypes.DataTextField = "TipoContrato";
            ddlContractTypes.DataValueField = "IdTipoContrato";
            ddlContractTypes.DataBind();
            ddlContractTypes.Items.Insert(0, new ListItem("Todos", "0"));
        }

        // Cargar ofertas activas y NO vencidas
        private void LoadJobs()
        {
            var jobs = _jobOffersService
                .ListarTodas()
                .Where(x =>
                    x.Estado == AttributesJobOfferts.EstadoOferta.Activa &&
                    x.FechaCierre >= DateTime.Now
                )
                .ToList();

            rptJobs.DataSource = jobs;
            rptJobs.DataBind();
        }

        // Aplicar filtros
        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            int idCategoria = int.Parse(ddlCategories.SelectedValue);
            int idModalidad = int.Parse(ddlWorkModes.SelectedValue);
            int idTipoContrato = int.Parse(ddlContractTypes.SelectedValue);

            var jobs = _jobOffersService
                .ListarTodas()
                .Where(x =>
                    x.Estado == AttributesJobOfferts.EstadoOferta.Activa &&
                    x.FechaCierre >= DateTime.Now
                )
                .ToList();

            if (idCategoria > 0)
                jobs = jobs.Where(x => x.IdCategoria == idCategoria).ToList();

            if (idModalidad > 0)
                jobs = jobs.Where(x => x.IdModalidadTrabajo == idModalidad).ToList();

            if (idTipoContrato > 0)
                jobs = jobs.Where(x => x.IdTipoContrato == idTipoContrato).ToList();

            rptJobs.DataSource = jobs;
            rptJobs.DataBind();
        }

        // Acciones del Repeater: Favoritos, Quitar Favorito, Postularme
        protected void rptJobs_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int idOferta = int.Parse(e.CommandArgument.ToString());
            int idEgresado = int.Parse(Session["IdEgresado"].ToString());

            using (var ctx = new RSContext())
            {
                var favorito = ctx.OfertasFavoritas
                    .FirstOrDefault(f => f.IdOferta == idOferta && f.IdEgresado == idEgresado);

                // Favoritos
                if (e.CommandName == "Favorito")
                {
                    if (favorito == null)
                    {
                        ctx.OfertasFavoritas.Add(new AttributesFavoritesJobs
                        {
                            IdEgresado = idEgresado,
                            IdOferta = idOferta,
                            FechaGuardado = DateTime.Now
                        });
                        ctx.SaveChanges();
                        MasterPage.MostrarModal("Éxito", "La oferta ha sido agregada a favoritos.");
                    }
                }

                // Quitar favorito
                if (e.CommandName == "QuitarFavorito")
                {
                    if (favorito != null)
                    {
                        ctx.OfertasFavoritas.Remove(favorito);
                        ctx.SaveChanges();
                        MasterPage.MostrarModal("Éxito", "La oferta ha sido eliminada de favoritos.");
                    }
                }

                // Postular
                if (e.CommandName == "Postularme")
                {
                    using (var context = new RSContext())
                    {
                        var postulado = ctx.Postulaciones
                            .FirstOrDefault(p => p.IdOferta == idOferta && p.IdEgresado == idEgresado);

                        if (postulado == null)
                        {
                            ctx.Postulaciones.Add(new AttributesApplications
                            {
                                IdOferta = idOferta,
                                IdEgresado = idEgresado,
                                FechaPostulacion = DateTime.Now,
                                EstadoFinal = AttributesApplications.EstadoPostulacion.Postulado
                            });
                            ctx.SaveChanges();

                            ScriptManager.RegisterStartupScript(
                                this, GetType(),
                                "postulacionExito",
                                "alert('¡Postulación exitosa!');",
                                true
                            );
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(
                                this, GetType(),
                                "postulacionExistente",
                                "alert('Ya estás postulado a esta oferta.');",
                                true
                            );
                        }
                    }
                }
            }

            LoadJobs();
        }
        // Mostrar botones según estado Favorito / Postulado
        protected void rptJobs_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item &&
                e.Item.ItemType != ListItemType.AlternatingItem)
                return;

            var data = (AttributesJobOfferts)e.Item.DataItem;
            int idOferta = data.IdOferta;
            int idEgresado = int.Parse(Session["IdEgresado"].ToString());

            var btnFav = (LinkButton)e.Item.FindControl("btnFavorito");
            var btnQuitar = (LinkButton)e.Item.FindControl("btnQuitarFavorito");
            var btnPostular = (LinkButton)e.Item.FindControl("btnPostularme");

            using (var ctx = new RSContext())
            {
                bool existeFavorito = ctx.OfertasFavoritas
                    .Any(f => f.IdOferta == idOferta && f.IdEgresado == idEgresado);

                bool yaPostulado = ctx.Postulaciones
                    .Any(p => p.IdOferta == idOferta && p.IdEgresado == idEgresado);

                btnFav.Visible = !existeFavorito;
                btnQuitar.Visible = existeFavorito;
                btnPostular.Visible = !yaPostulado;
            }
        }
    }
}
