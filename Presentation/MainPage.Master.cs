using System;
using System.Web.UI;

namespace Presentation
{
    public partial class MainPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AsignarNombreUsuario();
                MostrarEnlaceAdministrador();
            }
        }

        protected void lnkCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();      // Limpia todos los valores de la sesión
            Session.Abandon();
            Response.Redirect("~/Start/Login.aspx"); // Redirige a la página de login
        }

        private void AsignarNombreUsuario()
        {
            if (Session["NombreCompleto"] != null)
            {
                lblUsuario.Text = Session["NombreCompleto"].ToString();
            }
            else
            {
                lblUsuario.Text = "Invitado";
            }
        }

        private void MostrarEnlaceAdministrador()
        {
            string rol = Session["Rol"] as string;

            if (!string.IsNullOrEmpty(rol) && rol.Equals("Administrador", StringComparison.OrdinalIgnoreCase))
            {
                phAdminLink.Visible = true;
            }
            else
            {
                phAdminLink.Visible = false;
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
