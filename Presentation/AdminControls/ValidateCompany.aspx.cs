using LogicBusiness.Helpers;
using LogicBusiness.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation.AdminControls
{
    public partial class ValidateCompany : System.Web.UI.Page
    {
        private readonly UserService _userService = new UserService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEmpresas();
            }
        }
        // Propiedad para acceder al MasterPage y acceder a modales
        public MainPage MasterPage
        {
            get { return (MainPage)this.Master; }
        }

        private void CargarEmpresas()
        {
            var empresas = _userService.ObtenerEmpresasSinVerificar();

            gvEmpresas.DataSource = empresas.Select(e => new
            {
                e.IdUsuario, // este es el IdEmpresa real
                Nombre = e.Nombre ?? "(Sin nombre)",
                e.Correo,
                Ciudad = e.CiudadEmpresa ?? "(No especificada)",
                FechaRegistro = e.FechaRegistro,
                Telefono = e.TelefonoContacto ?? "(No especificado)",
                SectorIndustria = e.SectorIndustria ?? "(No especificado)",
                PersonaResp = e.PersonaRepresentante ?? "(No especificada)"
            }).ToList();

            gvEmpresas.DataBind();
        }

        protected void gvEmpresas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idEmpresa = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Aprobar")
            {
                var empresa = _userService.ObtenerUsuarioPorId(idEmpresa);
                if (empresa != null)
                {
                    empresa.Verificado = true;
                    _userService.ActualizarUsuario(empresa);

                    MasterPage.MostrarModal("Éxito", $"La empresa <b>{empresa.Nombre ?? "(Sin nombre)"}</b> ha sido aprobada correctamente.");
                    CargarEmpresas();
                }
            }
            else if (e.CommandName == "Rechazar")
            {
                var empresa = _userService.ObtenerUsuarioPorId(idEmpresa);
                if (empresa != null)
                {
                    empresa.Activo = false;
                    empresa.Verificado = false;
                    _userService.ActualizarUsuario(empresa);

                    MasterPage.MostrarModal("Éxito", $"La empresa <b>{empresa.Nombre ?? "(Sin nombre)"}</b> ha sido rechazada y desactivada.");
                    CargarEmpresas();
                }
            }

        }
    }
}