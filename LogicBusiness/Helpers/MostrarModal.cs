using System.Web.UI;
using System.Web.UI.WebControls;

namespace LogicBusiness.Helpers
{
    public static class MostrarModal
    {
        private const string IdModal = "modalMensaje";

        public static void Mostrar(Page page, string titulo, string mensaje)
        {
            if (page == null)
                return;

            var litTitulo = page.FindControl("litTituloModal") as Literal;
            var litMensaje = page.FindControl("litMensajeModal") as Literal;

            if (litTitulo != null)
                litTitulo.Text = titulo;

            if (litMensaje != null)
                litMensaje.Text = mensaje;

            string script = $@"
                var modal = new bootstrap.Modal(document.getElementById('{IdModal}'));
                modal.show();";

            ScriptManager.RegisterStartupScript(page, page.GetType(), "MostrarModal_" + IdModal, script, true);
        }

        public static void MostrarConRedireccion(Page page, string titulo, string mensaje, string urlRedireccion)
        {
            if (page == null || string.IsNullOrWhiteSpace(urlRedireccion))
                return;

            var litTitulo = page.FindControl("litTituloModal") as Literal;
            var litMensaje = page.FindControl("litMensajeModal") as Literal;

            if (litTitulo != null)
                litTitulo.Text = titulo;

            if (litMensaje != null)
                litMensaje.Text = mensaje;

            string script = $@"
                var modal = new bootstrap.Modal(document.getElementById('{IdModal}'));
                modal.show();

                var btnCerrar = document.querySelector('#' + '{IdModal}' + ' .btn[data-bs-dismiss=""modal""]');
                if (btnCerrar) {{
                    btnCerrar.addEventListener('click', function() {{
                        window.location.href = '{urlRedireccion}';
                    }});
                }}";

            ScriptManager.RegisterStartupScript(page, page.GetType(), "MostrarModalRedirect_" + IdModal, script, true);
        }
    }
}
