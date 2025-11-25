using System;
using System.IO;
using System.Web.UI;
using Common.Attributes;
using LogicBusiness.Service;

namespace Presentation.Events
{
    public partial class AddEvents : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Aquí puedes inicializar controles si es necesario
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos())
                    return;

                var nuevoEvento = CrearEventoDesdeFormulario();
                ManejarImagenEvento(nuevoEvento);

                // Guardar en la base de datos
                GuardarEventoBD(nuevoEvento);

                MostrarModal("Evento guardado correctamente.");
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MostrarModal("Ocurrió un error: " + ex.Message);
            }
        }

        #region Métodos de validación y creación de evento

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtTitulo.Text) || string.IsNullOrWhiteSpace(txtFechaInicio.Text))
            {
                MostrarModal("Debes llenar al menos el título y la fecha de inicio.");
                return false;
            }
            return true;
        }

        private AttributesEvents CrearEventoDesdeFormulario()
        {
            return new AttributesEvents
            {
                Titulo = txtTitulo.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim(),
                FechaInicio = DateTime.Parse(txtFechaInicio.Text),
                FechaFin = string.IsNullOrWhiteSpace(txtFechaFin.Text) ? (DateTime?)null : DateTime.Parse(txtFechaFin.Text),
                FechaCreacion = DateTime.Now,
                Lugar = txtLugar.Text.Trim(),
                Tipo = ddlTipo.SelectedValue,
                Organizador = txtOrganizador.Text.Trim(),
            };
        }

        private void ManejarImagenEvento(AttributesEvents evento)
        {
            string rutaBase = Server.MapPath("../Resources/events/");
            if (!Directory.Exists(rutaBase))
                Directory.CreateDirectory(rutaBase);

            string nombreArchivo = "default.png";

            if (fuImagen.HasFile)
            {
                string extension = Path.GetExtension(fuImagen.FileName);
                nombreArchivo = Guid.NewGuid().ToString() + extension;
                string rutaCompleta = Path.Combine(rutaBase, nombreArchivo);
                fuImagen.SaveAs(rutaCompleta);
            }

            evento.RutaImagen = $"../Resources/events/{nombreArchivo}";
        }

        #endregion

        #region Métodos auxiliares

        private void LimpiarCampos()
        {
            txtTitulo.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtFechaInicio.Text = string.Empty;
            txtFechaFin.Text = string.Empty;
            txtLugar.Text = string.Empty;
            ddlTipo.SelectedIndex = 0;
            txtOrganizador.Text = string.Empty;
        }

        private void GuardarEventoBD(AttributesEvents evento)
        {
            var service = new EventsService();
            service.CrearEvento(evento);
        }

        private void MostrarModal(string mensaje)
        {
            string script = $@"
                document.getElementById('mensajeModalBody').innerText = '{mensaje.Replace("'", "\\'")}';
                var modal = new bootstrap.Modal(document.getElementById('mensajeModal'));
                modal.show();";
            ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModal", script, true);
        }

        #endregion
    }
}
