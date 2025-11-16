using Common.Attributes;
using LogicBusiness.Helpers;
using LogicBusiness.Service;
using System;
using System.Web.UI;


namespace Presentation
{
    public partial class Login : System.Web.UI.Page
    {
        private readonly UserService _userService = new UserService();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Evitar que se recargue con postback sin necesidad
            if (!IsPostBack)
            {
                Session.Clear();
            }
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            string correo = txtCorreo.Text.Trim();
            string clave = txtClave.Text.Trim();

            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(clave))
            {
                MostrarModal("Error", "Por favor complete todos los campos.");
                return;
            }

            var usuario = _userService.ValidarUsuario(correo, clave);



            if (usuario != null && usuario.Activo)
            {
                // Guardamos datos de sesión
                Session["IdUsuario"] = usuario.IdUsuario;
                Session["NombreCompleto"] = usuario.Nombre;
                Session["Rol"] = usuario.Rol;
                Session["Verificado"] = usuario.Verificado;

                if (usuario.Rol == "Empresa" && usuario.Verificado == false)
                {
                    MostrarModal("Atención", "Su cuenta de empresa aún no ha sido verificada. Por favor, espere la confirmación antes de acceder.");
                    return;
                }
                // Redirigir a el foro de publicaciones
                Response.Redirect("../Forum/Publications.aspx");

            }
            else
            {
                MostrarModal("Error", "Usuario o contraseña incorrectos o usuario inactivo.");
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
