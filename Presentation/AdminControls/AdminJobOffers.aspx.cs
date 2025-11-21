using Common.Attributes;
using LogicBusiness.Service;
using System;
using System.Linq;

namespace Presentation
{
    public partial class AdminJobOffers : System.Web.UI.Page
    {
        private readonly JobOffertsService _jobService = new JobOffertsService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarOfertasPendientes();
            }
        }

        private void CargarOfertasPendientes()
        {
            var ofertasPendientes = _jobService.ListarTodas()
                                    .Where(o => o.Estado == AttributesJobOfferts.EstadoOferta.PorVerificar)
                                    .ToList();

            gvOfertasPendientes.DataSource = ofertasPendientes;
            gvOfertasPendientes.DataBind();
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            int idOferta = Convert.ToInt32(((System.Web.UI.WebControls.Button)sender).CommandArgument);
            var oferta = _jobService.ObtenerPorId(idOferta);
            if (oferta != null)
            {
                oferta.Estado = AttributesJobOfferts.EstadoOferta.Activa;
                _jobService.Actualizar(oferta);

                lblMensaje.Text = "Oferta aceptada correctamente.";
                lblMensaje.CssClass = "alert alert-success";

                CargarOfertasPendientes();
            }
        }

        protected void btnRechazar_Click(object sender, EventArgs e)
        {
            int idOferta = Convert.ToInt32(((System.Web.UI.WebControls.Button)sender).CommandArgument);
            var oferta = _jobService.ObtenerPorId(idOferta);
            if (oferta != null)
            {
                oferta.Estado = AttributesJobOfferts.EstadoOferta.Cerrada;
                _jobService.Actualizar(oferta);

                lblMensaje.Text = "Oferta rechazada correctamente.";
                lblMensaje.CssClass = "alert alert-warning";

                CargarOfertasPendientes();
            }
        }
    }
}
