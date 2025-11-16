using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Entities;
using LogicBusiness.Helpers;
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

            var resultado = _userService.RegistrarEmpresa(usuario);

            if (resultado)
            {
                MostrarModalRedireccion("Éxito", "¡Solicitud enviada con éxito! Tu registro será revisado y confirmado pronto.",
                    "../Start/Login.aspx");
            }
            else
            {
                MostrarModal("Error", "Error al registrar la empresa. Por favor, verifica los datos.");
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