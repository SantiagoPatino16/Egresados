using Presentation.CreateOffer;
using System;
using System.Web.UI;

namespace Presentation
{
    public partial class MainPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Mostrar nombre de usuario y enlaces según rol
            AsignarNombreUsuario();
            MostrarEnlacesSegunRol();
        }

        /// <summary>
        /// Asigna el nombre del usuario al label del dropdown
        /// </summary>
        private void AsignarNombreUsuario()
        {
            lblUsuario.Text = (Session["NombreCompleto"] as string) ?? "Invitado";
        }

        private void MostrarEnlacesSegunRol()
        {
            string rol = (Session["Rol"] as string)?.Trim() ?? string.Empty;

            //visible para Egresados
            phBolsaEmpleo.Visible = rol.Equals("Egresado", StringComparison.OrdinalIgnoreCase);
            phEgresadooLink.Visible = rol.Equals("Egresado", StringComparison.OrdinalIgnoreCase);

            //solo visible para Empresa
            phJobApplicants.Visible = rol.Equals("Empresa", StringComparison.OrdinalIgnoreCase);
            phEmploymentCreate.Visible = rol.Equals("Empresa", StringComparison.OrdinalIgnoreCase);

            //visible para Administradores
            phAdminLink.Visible = rol.Equals("Administrador", StringComparison.OrdinalIgnoreCase);
        }

        protected void lnkCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Start/Login.aspx");
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

        /// <summary>
        /// Muestra un modal y redirige al cerrar
        /// </summary>
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
