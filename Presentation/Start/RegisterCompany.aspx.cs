using System;
using System.Web.UI;
using Common.Entities;
using LogicBusiness.Service;

namespace Presentation.Start
{
    public partial class RegisterCompany : System.Web.UI.Page
    {
        private readonly UserService _userService = new UserService();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                // VALIDACIÓN BÁSICA
                if (string.IsNullOrWhiteSpace(txtNombreEmpresa.Text) ||
                    string.IsNullOrWhiteSpace(txtNIT.Text) ||
                    string.IsNullOrWhiteSpace(txtRepresentante.Text) ||
                    string.IsNullOrWhiteSpace(txtCargo.Text) ||
                    string.IsNullOrWhiteSpace(txtTelefono.Text) ||
                    string.IsNullOrWhiteSpace(txtCiudad.Text) ||
                    string.IsNullOrWhiteSpace(txtSector.Text) ||
                    string.IsNullOrWhiteSpace(txtDescripcion.Text) ||
                    string.IsNullOrWhiteSpace(txtCorreo.Text) ||
                    string.IsNullOrWhiteSpace(txtClave.Text))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Por favor complete todos los campos.');", true);
                    return;
                }

                var usuario = new AttributesUser
                {
                    Nombre = txtNombreEmpresa.Text.Trim(),
                    Nit = txtNIT.Text.Trim(),
                    PersonaRepresentante = txtRepresentante.Text.Trim(),
                    CargoRepresentante = txtCargo.Text.Trim(),
                    TelefonoContacto = txtTelefono.Text.Trim(),
                    CiudadEmpresa = txtCiudad.Text.Trim(),
                    SectorIndustria = txtSector.Text.Trim(),
                    DescripcionEmpresa = txtDescripcion.Text.Trim(),
                    Correo = txtCorreo.Text.Trim(),
                    ClaveHash = txtClave.Text.Trim(),
                    Rol = "Empresa",
                    FechaRegistro = DateTime.Now,
                    Activo = true,
                    Verificado = false
                };

                bool resultado = _userService.RegistrarEmpresa(usuario);

                if (resultado)
                {
                    LimpiarCampos();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('¡Solicitud enviada con éxito!');", true);
                    Response.Redirect("../Start/Login.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Error al registrar la empresa.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Ocurrió un error: " + ex.Message + "');", true);
            }
        }

        private void LimpiarCampos()
        {
            txtNombreEmpresa.Text = "";
            txtNIT.Text = "";
            txtRepresentante.Text = "";
            txtCargo.Text = "";
            txtTelefono.Text = "";
            txtCiudad.Text = "";
            txtSector.Text = "";
            txtDescripcion.Text = "";
            txtCorreo.Text = "";
            txtClave.Text = "";
        }
    }
}
