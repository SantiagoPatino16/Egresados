using Common.Entities;
using LogicBusiness.Helpers;
using LogicBusiness.Service;
using System;
using System.Web.UI;

namespace Presentation
{
    public partial class Register : System.Web.UI.Page
    {
        private readonly UserService userService = new UserService();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = ""; // Limpiar mensaje previo

            try
            {
                // Validar campos básicos
                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtCorreo.Text) ||
                    string.IsNullOrWhiteSpace(txtClave.Text))
                {
                    lblMensaje.Text = "Por favor complete todos los campos.";
                    return;
                }

                var nuevoUsuario = new AttributesUser
                {
                    Nombre = txtNombre.Text.Trim(),
                    Correo = txtCorreo.Text.Trim(),
                    ClaveHash = txtClave.Text.Trim(), // Se encripta internamente
                    Rol = ddlRol.SelectedValue,
                    FechaRegistro = DateTime.Now,
                    Activo = true
                };

                bool registrado = userService.RegistrarUsuario(nuevoUsuario);

                if (registrado)
                {
                    lblMensaje.CssClass = "text-green-600 font-semibold";
                    lblMensaje.Text = "Usuario registrado correctamente.";

                    // Limpiar campos
                    txtNombre.Text = "";
                    txtCorreo.Text = "";
                    txtClave.Text = "";

                    // Redirigir al login
                    Response.Redirect("../Start/Login.aspx");
                }
                else
                {
                    lblMensaje.CssClass = "text-red-600 font-semibold";
                    lblMensaje.Text = "El correo ya está registrado.";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.CssClass = "text-red-600 font-semibold";
                lblMensaje.Text = "Ocurrió un error: " + ex.Message;
            }
        }
    }
}
