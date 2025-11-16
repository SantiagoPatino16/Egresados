using Common.Entities;
using LogicBusiness.Helpers;
using LogicBusiness.Service;
using System;
using System.Security.Policy;
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
            try
            {
                var nuevoUsuario = new AttributesUser
                {
                    Nombre = txtNombre.Text.Trim(),
                    Correo = txtCorreo.Text.Trim(),
                    ClaveHash = txtClave.Text.Trim(), // Se encripta en la función de inserción.
                    Rol = ddlRol.SelectedValue,
                    FechaRegistro = DateTime.Now,
                    Activo = true
                };

                bool registrado = userService.RegistrarUsuario(nuevoUsuario);


                if (registrado)
                {
                    MostrarModalRedireccion("Éxito", "Usuario registrado correctamente.", "../Start/Login.aspx");

                    txtNombre.Text = string.Empty;
                    txtCorreo.Text = string.Empty;
                    txtClave.Text = string.Empty;
                }
                else
                {

                    MostrarModal("Error", "El correo ya está registrado.", "Aceptar");
                }
            }
            catch (Exception ex)
            {

                MostrarModal("Error", "Ocurrió un error: " + ex.Message);
            }
        }
        public void MostrarModal(string titulo, string mensaje, string textoBoton = "Cerrar")
        {
            litTituloModal.Text = titulo;
            litMensajeModal.Text = mensaje;
            litTextoBotonModal.Text = textoBoton;

            string script = @"
        var modal = document.getElementById('modalMensaje');
        modal.removeAttribute('data-url');
        var bsModal = new bootstrap.Modal(modal);
        bsModal.show();";

            ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModal", script, true);
        }

        public void MostrarModalRedireccion(string titulo, string mensaje, string urlRedireccion, string textoBoton = "Continuar")
        {
            litTituloModal.Text = titulo;
            litMensajeModal.Text = mensaje;
            litTextoBotonModal.Text = textoBoton;

            string script = $@"
        var modal = document.getElementById('modalMensaje');
        modal.setAttribute('data-url', '{urlRedireccion}');
        var bsModal = new bootstrap.Modal(modal);
        bsModal.show();

        var btnCerrar = modal.querySelector('.btn[data-bs-dismiss=""modal""]');
        if (btnCerrar) {{
            btnCerrar.addEventListener('click', function() {{
                window.location.href = '{urlRedireccion}';
            }});
        }}";

            ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModalRedirect", script, true);
        }

    }


}

