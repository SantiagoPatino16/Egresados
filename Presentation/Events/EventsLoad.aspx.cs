using LogicBusiness.Service;
using System;
using System.Web.UI;

namespace Presentation.Events
{
    public partial class EventsLoad : System.Web.UI.Page
    {
        private readonly EventsService _service = new EventsService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RegistrarScriptModal();
                CargarEventos();
            }
        }

        // Carga de eventos con filtros opcionales
        private void CargarEventos()
        {
            try
            {
                string titulo = txtBuscarTitulo.Text.Trim();
                string fechaInicio = txtFechaInicioFiltro.Text;
                string fechaFin = txtFechaFinFiltro.Text;

                var eventos = _service.FiltrarEventos(titulo, fechaInicio, fechaFin);

                if (eventos != null && eventos.Count > 0)
                {
                    rptEventos.DataSource = eventos;
                    rptEventos.DataBind();
                    lblSinEventos.Visible = false;
                }
                else
                {
                    rptEventos.DataSource = null;
                    rptEventos.DataBind();
                    lblSinEventos.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblSinEventos.Text = "Ocurrió un error al cargar los eventos: " + ex.Message;
                lblSinEventos.Visible = true;
            }
        }


        // 🔹 Botón de búsqueda / filtrado
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarEventos();
        }

        private void RegistrarScriptModal()
        {
            string script = @"
                function cargarDetallesEvento(btn) {
                    document.getElementById('tituloEventoModal').innerText = btn.getAttribute('data-titulo');
                    document.getElementById('descripcionEventoModal').innerText = btn.getAttribute('data-descripcion');
                    document.getElementById('lugarEventoModal').innerText = btn.getAttribute('data-lugar');
                    document.getElementById('fechaEventoModal').innerText = btn.getAttribute('data-fechainicio') + 
                        (btn.getAttribute('data-fechafin') ? ' - ' + btn.getAttribute('data-fechafin') : '');
                    document.getElementById('organizadorEventoModal').innerText = btn.getAttribute('data-organizador');
                    document.getElementById('imgEventoModal').src = btn.getAttribute('data-imagen');
                }";

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalEventoScript", script, true);
        }

        protected void btnLimpiarCampos_Click(object sender, EventArgs e)
        {
            // Limpiar los campos de texto
            txtBuscarTitulo.Text = string.Empty;
            txtFechaInicioFiltro.Text = string.Empty;
            txtFechaFinFiltro.Text = string.Empty;

            // Ocultar el mensaje de sin resultados si estaba visible
            lblSinEventos.Visible = false;

            // Volver a cargar todos los eventos
            CargarEventos();
        }

    }
}
