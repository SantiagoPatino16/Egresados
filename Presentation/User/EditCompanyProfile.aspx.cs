using LogicBusiness.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation.User
{
    public partial class EditCompanyProfile : System.Web.UI.Page
    {
        private readonly UserService _userService = new UserService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarDatosUsuario();
        }

        // Propiedad para acceder al MasterPage y acceder a modales
        public MainPage MasterPage
        {
            get { return (MainPage)this.Master; }
        }

        private void CargarDatosUsuario()
        {
            try
            {
                if (Session["IdUsuario"] == null)
                {
                    MasterPage.MostrarModal("Error", "Sesión expirada. Vuelve a iniciar sesión.", "Aceptar");
                    return;
                }

                int idUsuario = Convert.ToInt32(Session["IdUsuario"]);
                var usuario = _userService.ObtenerUsuarioPorId(idUsuario);

                if (usuario == null)
                {
                    MasterPage.MostrarModal("Error", "No se encontró la información del usuario.", "Aceptar");
                    return;
                }

                //Cargar datos en los campos
                txtNit.Text = usuario.Nit;
                txtNombreEmpresa.Text = usuario.Nombre;
                txtDescripcionEmpresa.Text = usuario.DescripcionEmpresa;
                txtUbicacionEmpresa.Text = usuario.UbicacionEmpresa;
                txtCiudadEmpresa.Text = usuario.CiudadEmpresa;

                txtPersonaRepresentante.Text = usuario.PersonaRepresentante;
                txtCargoRepresentante.Text = usuario.CargoRepresentante;
                txtTelefonoContacto.Text = usuario.TelefonoContacto;
                txtSectorIndustria.Text = usuario.SectorIndustria;

            }
            catch (Exception ex)
            {
                MasterPage.MostrarModal("Error", "Error al cargar los datos del usuario: " + ex.Message, "Aceptar");
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["IdUsuario"] == null)
                {
                    MasterPage.MostrarModal("Error", "Sesión expirada. Vuelve a iniciar sesión.", "Aceptar");
                    return;
                }

                int idUsuario = Convert.ToInt32(Session["IdUsuario"]);
                var usuario = _userService.ObtenerUsuarioPorId(idUsuario);

                if (usuario == null)
                {
                    MasterPage.MostrarModal("Error", "No se encontró el usuario.", "Aceptar");
                    return;
                }

                // Actualizar datos modificables
                usuario.Nit = txtNit.Text.Trim();
                usuario.Nombre = txtNombreEmpresa.Text.Trim();
                usuario.DescripcionEmpresa = txtDescripcionEmpresa.Text.Trim();
                usuario.UbicacionEmpresa = txtUbicacionEmpresa.Text.Trim();
                usuario.CiudadEmpresa = txtCiudadEmpresa.Text.Trim();

                usuario.PersonaRepresentante = txtPersonaRepresentante.Text.Trim();
                usuario.CargoRepresentante = txtCargoRepresentante.Text.Trim();
                usuario.TelefonoContacto = txtTelefonoContacto.Text.Trim();
                usuario.SectorIndustria = txtSectorIndustria.Text.Trim();


                // Guardar cambios
                _userService.ActualizarUsuario(usuario);

                MasterPage.MostrarModalRedireccion("Éxito", "Los cambios se han guardado correctamente.", "Aceptar","../User/Profile.aspx");
            }
            catch (Exception ex)
            {
                MasterPage.MostrarModal("Error", "Error al guardar los cambios: " + ex.Message, "Aceptar");
            }
        }
    }
}