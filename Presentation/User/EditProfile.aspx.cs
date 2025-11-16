using Common.Entities;
using LogicBusiness.Service;
using System;
using System.Web.UI;

namespace Presentation.User
{
    public partial class EditProfile : Page
    {
        private readonly UserService _userService = new UserService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarDatosUsuario();
        }

        // Propiedad para acceder al MasterPage y acceder a modales
        public MainPage MasterPage
        {
            get { return (MainPage)this.Master; }
        }

        private void CargarDatosUsuario()
        {
            try
            {
                if (Session["IdUsuario"] == null)
                {
                    MasterPage.MostrarModal("Error","Sesión expirada. Vuelve a iniciar sesión.", "Aceptar");
                    return;
                }

                int idUsuario = Convert.ToInt32(Session["IdUsuario"]);
                var usuario = _userService.ObtenerUsuarioPorId(idUsuario);

                if (usuario == null)
                {
                    MasterPage.MostrarModal("Error", "No se encontró la información del usuario.", "Aceptar");
                    return;
                }

                // Cargar datos en los campos
                txtNombreCompleto.Text = usuario.Nombre;
                txtCiudadResidencia.Text = usuario.CiudadResidencia;
                txtProgramaAcademico.Text = usuario.ProgramaAcademico;
                txtAnioGraduacion.Text = usuario.AnioGraduacion > 0 ? usuario.AnioGraduacion.ToString() : string.Empty;
                txtEmpresaActual.Text = usuario.EmpresaActual;
                txtCargoActual.Text = usuario.CargoActual;
                txtLinkedIn.Text = usuario.LinkedIn;
                txtSitioWeb.Text = usuario.SitioWebPersonal;
                txtBiografia.Text = usuario.Biografia;
            }
            catch (Exception ex)
            {
                MasterPage.MostrarModal("Error", "Error al cargar los datos del usuario: " + ex.Message, "Aceptar");
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["IdUsuario"] == null)
                {
                    MasterPage.MostrarModal("Error", "Sesión expirada. Vuelve a iniciar sesión.", "Aceptar");
                    return;
                }

                int idUsuario = Convert.ToInt32(Session["IdUsuario"]);
                var usuario = _userService.ObtenerUsuarioPorId(idUsuario);

                if (usuario == null)
                {
                    MasterPage.MostrarModal("Error", "No se encontró el usuario.", "Aceptar");
                    return;
                }

                // Actualizar datos modificables
                usuario.Nombre = txtNombreCompleto.Text.Trim();
                usuario.CiudadResidencia = txtCiudadResidencia.Text.Trim();
                usuario.ProgramaAcademico = txtProgramaAcademico.Text.Trim();
                usuario.AnioGraduacion = int.TryParse(txtAnioGraduacion.Text, out int anio) ? anio : 0;
                usuario.EmpresaActual = txtEmpresaActual.Text.Trim();
                usuario.CargoActual = txtCargoActual.Text.Trim();
                usuario.LinkedIn = txtLinkedIn.Text.Trim();
                usuario.SitioWebPersonal = txtSitioWeb.Text.Trim();
                usuario.Biografia = txtBiografia.Text.Trim();

                // Guardar cambios
                _userService.ActualizarUsuario(usuario);

                MasterPage.MostrarModal("Éxito", "Los cambios se han guardado correctamente.", "Aceptar");
            }
            catch (Exception ex)
            {
                MasterPage.MostrarModal("Error", "Error al guardar los cambios: " + ex.Message, "Aceptar");
            }
        }



    }
}
