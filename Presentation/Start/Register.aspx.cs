using System;
using LogicBusiness.Service;
using Common.Entities;

namespace Presentation
{
    public partial class Register : System.Web.UI.Page
    {
        private readonly UserService userService = new UserService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMensaje.Text = string.Empty;
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                var nuevoUsuario = new AttributesUser
                {
                    NombreCompleto = txtNombre.Text.Trim(),
                    Correo = txtCorreo.Text.Trim(),
                    ClaveHash = txtClave.Text.Trim(), // Se encripta en la función de inserción.
                    Rol = ddlRol.SelectedValue,
                    FechaRegistro = DateTime.Now,
                    Activo = true
                };

                bool registrado = userService.RegistrarUsuario(nuevoUsuario);

                if (registrado)
                {
                    lblMensaje.CssClass = "text-success";
                    lblMensaje.Text = "✅ Usuario registrado correctamente.";

                    txtNombre.Text = string.Empty;
                    txtCorreo.Text = string.Empty;
                    txtClave.Text = string.Empty;
                }
                else
                {
                    lblMensaje.CssClass = "text-danger";
                    lblMensaje.Text = "❌ El correo ya está registrado.";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }

    }
}
