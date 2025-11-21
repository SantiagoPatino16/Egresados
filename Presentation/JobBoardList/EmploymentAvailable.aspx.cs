using Common.Attributes;
using Common.Entities;
using DataAccess.ConnectionDB;
using LogicBusiness.Service;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation.JobBoardList
{
    public partial class EmploymentAvailable : Page
    {
        private JobOffertsService _jobOffersService = new JobOffertsService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarFiltros();
                LoadJobs();
            }
        }
        MainPage MasterPage
        {
            get { return (MainPage)this.Master; }
        }
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
        private void LoadJobs()
        {
            var jobs = _jobOffersService
                .ListarTodas()
                .Where(x => x.Estado == AttributesJobOfferts.EstadoOferta.Activa)
                .ToList();

            rptJobs.DataSource = jobs;
            rptJobs.DataBind();
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            int idCategoria = int.Parse(ddlCategories.SelectedValue);
            int idModalidad = int.Parse(ddlWorkModes.SelectedValue);
            int idTipoContrato = int.Parse(ddlContractTypes.SelectedValue);

            var jobs = _jobOffersService
                .ListarTodas()
                .Where(x => x.Estado == AttributesJobOfferts.EstadoOferta.Activa)
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

        // AGREGAR O QUITAR FAVORITO
        protected void rptJobs_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int idOferta = int.Parse(e.CommandArgument.ToString());
            int idEgresado = int.Parse(Session["IdEgresado"].ToString());

            using (var ctx = new RSContext())
            {
                var favorito = ctx.OfertasFavoritas
                    .FirstOrDefault(f => f.IdOferta == idOferta && f.IdEgresado == idEgresado);

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

                if (e.CommandName == "QuitarFavorito")
                {
                    if (favorito != null)
                    {
                        ctx.OfertasFavoritas.Remove(favorito);
                        ctx.SaveChanges();

                        ScriptManager.RegisterStartupScript(this, GetType(), "ok2",
                            "alert('Oferta eliminada de favoritos');", true);
                    }
                }
            }
            LoadJobs();
        }

        // MOSTRAR ESTADO FAVORITO / NO FAVORITO
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

            using (var ctx = new RSContext())
            {
                bool existe = ctx.OfertasFavoritas
                    .Any(f => f.IdOferta == idOferta && f.IdEgresado == idEgresado);

                btnFav.Visible = !existe;
                btnQuitar.Visible = existe;
            }
        }
    }
}