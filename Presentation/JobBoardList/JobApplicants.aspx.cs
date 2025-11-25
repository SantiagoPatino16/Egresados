using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicBusiness.Service;
using Common.Attributes;
using System.Linq;

namespace Presentation.JobBoardList
{
    public partial class JobApplicants : Page
    {
        ApplicationsService service = new ApplicationsService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPostulantes();
            }
        }

        private void CargarPostulantes()
        {
            if (Session["IdEmpresa"] == null)
            {
                alertEstado.InnerHtml = "No se encontró el IdEmpresa en sesión.";
                alertEstado.Visible = true;
                return;
            }

            int idEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            var listaPostulantes = service.ListarPorEmpresa(idEmpresa);

            if (listaPostulantes == null || !listaPostulantes.Any())
            {
                alertEstado.InnerHtml = "No se encontraron postulantes para esta empresa.";
                alertEstado.Visible = true;

                gvPostulantes.DataSource = null;
                gvPostulantes.DataBind();
                return;
            }

            alertEstado.Visible = false;

            gvPostulantes.DataSource = listaPostulantes;
            gvPostulantes.DataBind();
        }

        protected void gvPostulantes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CambiarEstado")
            {
                string[] args = e.CommandArgument.ToString().Split(';');
                int idPostulacion = Convert.ToInt32(args[0]);
                string nuevoEstado = args[1];

                var postulacion = service.ObtenerPorId(idPostulacion);

                if (postulacion != null)
                {
                    postulacion.EstadoFinal =
                        (AttributesApplications.EstadoPostulacion)
                        Enum.Parse(typeof(AttributesApplications.EstadoPostulacion), nuevoEstado);

                    service.Actualizar(postulacion);

                    // historial
                    AttributesStatusApplication historial = new AttributesStatusApplication
                    {
                        IdPostulacion = idPostulacion,
                        Estado = nuevoEstado == "Aceptada"
                          ? AttributesStatusApplication.EstadoPostulacionEnum.Finalizado
                          : AttributesStatusApplication.EstadoPostulacionEnum.EnProceso,
                        FechaPostulacion = DateTime.Now
                    };

                    service.CrearEstado(historial);
                }

                alertEstado.InnerHtml = $"El estado fue cambiado a <strong>{nuevoEstado}</strong>.";
                alertEstado.Visible = true;

                CargarPostulantes();
            }
        }

        protected void gvPostulantes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // obtener enum real del modelo
                var estadoEnum = (AttributesApplications.EstadoPostulacion)
                                 DataBinder.Eval(e.Row.DataItem, "EstadoFinal");

                Button btnAceptar = (Button)e.Row.FindControl("btnAceptar");
                Button btnRechazar = (Button)e.Row.FindControl("btnRechazar");

                // mostrar botones SOLO si está Postulado
                if (estadoEnum == AttributesApplications.EstadoPostulacion.Postulado)
                {
                    if (btnAceptar != null) btnAceptar.Visible = true;
                    if (btnRechazar != null) btnRechazar.Visible = true;
                }
                else
                {
                    if (btnAceptar != null) btnAceptar.Visible = false;
                    if (btnRechazar != null) btnRechazar.Visible = false;
                }
            }
        }
    }
}
