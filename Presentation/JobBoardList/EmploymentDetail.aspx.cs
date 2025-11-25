using LogicBusiness.Service;
using System;

namespace Presentation.JobBoardList
{
    public partial class EmploymentDetail : System.Web.UI.Page
    {
        private readonly JobOffertsService service = new JobOffertsService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDetalle();
            }
        }

        private void CargarDetalle()
        {
            // Validar id
            if (!int.TryParse(Request.QueryString["id"], out int id))
            {
                pnlError.Visible = true;
                return;
            }

            var oferta = service.ObtenerPorId(id);

            if (oferta == null)
            {
                pnlError.Visible = true;
                return;
            }

            pnlDetalle.Visible = true;

            // Datos principales
            litTitulo.Text = oferta.Titulo;
            litDescripcion.Text = oferta.Descripcion;
            litRequisitos.Text = oferta.Requisitos;
            litCiudad.Text = oferta.Ciudad;
            litSalario.Text = oferta.Salario.ToString("N0");

            // Datos adicionales de tu tabla OfertasEmpleo

            litFechaPublicacion.Text = oferta.FechaPublicacion.ToString("dd/MM/yyyy");
            litFechaCierre.Text = oferta.FechaCierre.ToString("dd/MM/yyyy");
            litEstado.Text = oferta.Estado.ToString();
        }
    }
}
