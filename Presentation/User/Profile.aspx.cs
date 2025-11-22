using LogicBusiness.Helpers;
using LogicBusiness.Service;
using System;
using System.Security.Policy;
using System.Web;
using System.Web.UI;

namespace Presentation
{
    public partial class Perfil : Page
    {
        private readonly UserService _userService = new UserService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarPerfil();
        }
        // Propiedad para acceder al MasterPage y acceder a modales
        public MainPage MasterPage
        {
            get { return (MainPage)this.Master; }
        }

        private void CargarPerfil()
        {
            try
            {
                // Verificar si se está viendo el perfil de otro usuario
                int idUsuario;
                bool esPerfilPropio = true;
                
                if (Request.QueryString["id"] != null && int.TryParse(Request.QueryString["id"], out int idOtroUsuario))
                {
                    // Ver perfil de otro usuario
                    idUsuario = idOtroUsuario;
                    esPerfilPropio = false;
                }
                else
                {
                    // Ver perfil propio
                    if (Session["IdUsuario"] == null)
                    {
                        Response.Redirect("~/Start/Login.aspx");
                        return;
                    }
                    idUsuario = Convert.ToInt32(Session["IdUsuario"]);
                }
                
                var usuario = _userService.ObtenerUsuarioPorId(idUsuario);

                if (usuario == null)
                {
                    MasterPage.MostrarModal("Error", "No se encontró el usuario.", "Aceptar");
                    return;
                }
                
                // Ocultar botones de edición si no es el perfil propio
                if (!esPerfilPropio)
                {
                    btnEditarPerfil.Visible = false;
                    btnCambiarFoto.Visible = false;
                    fuFotoPerfil.Visible = false;
                }

                // Información principal
                litNombre.Text = usuario.Nombre;
                litCorreo.Text = usuario.Correo;
                litRol.Text = usuario.Rol;
                litFechaRegistro.Text = usuario.FechaRegistro.ToString("dd/MM/yyyy");
                litActivo.Text = usuario.Activo ? "Sí" : "No";

                // Detalles del perfil 
                litPrograma.Text = usuario.ProgramaAcademico ?? "Sin información";
                litAnioGraduacion.Text = usuario.AnioGraduacion == 0 ? "No especificado" : usuario.AnioGraduacion.ToString();
                litEmpresa.Text = usuario.EmpresaActual ?? "Sin información";
                litCargo.Text = usuario.CargoActual ?? "Sin información";
                litCiudad.Text = usuario.CiudadResidencia ?? "Sin información";
                litUltimaConexion.Text = usuario.UltimaConexion?.ToString("dd/MM/yyyy HH:mm") ?? "Sin registro";
                litBiografia.Text = usuario.Biografia ?? "Sin biografía disponible.";

                // Enlaces seguros (codificados)
                if (!string.IsNullOrEmpty(usuario.LinkedIn))
                {
                    string urlLinkedIn = HttpUtility.HtmlEncode(usuario.LinkedIn);
                    litLinkedIn.Text = $"<a href='{urlLinkedIn}' target='_blank' rel='noopener noreferrer'>Perfil de LinkedIn</a>";
                }
                else
                {
                    litLinkedIn.Text = "No registrado";
                }

                if (!string.IsNullOrEmpty(usuario.SitioWebPersonal))
                {
                    string urlWeb = HttpUtility.HtmlEncode(usuario.SitioWebPersonal);
                    litSitioWeb.Text = $"<a href='{urlWeb}' target='_blank' rel='noopener noreferrer'>Sitio personal</a>";
                }
                else
                {
                    litSitioWeb.Text = "No registrado";
                }

                // Imagen de perfil
                imgFotoPerfil.ImageUrl = !string.IsNullOrEmpty(usuario.FotoPerfil)
                    ? usuario.FotoPerfil
                    : "../Resources/perfiles/default.png";
            }
            catch (Exception ex)
            {
                MasterPage.MostrarModal("Error", "Error al cargar el perfil: " + ex.Message, "Aceptar");
            }
        }


        protected void btnCambiarFoto_Click(object sender, EventArgs e)
        {
            if (!fuFotoPerfil.HasFile)
            {
                MasterPage.MostrarModal("Advertencia", "Debe seleccionar una foto antes de subirla.", "Aceptar");
                return;
            }

            try
            {
                int idUsuario = Convert.ToInt32(Session["IdUsuario"]);
                string ruta = UploadFile.GuardarImagen(fuFotoPerfil.PostedFile, "perfiles", $"user_{idUsuario}");

                _userService.ActualizarFotoPerfil(idUsuario, ruta);
                imgFotoPerfil.ImageUrl = ruta;

                MasterPage.MostrarModal("Éxito", "Foto de perfil actualizada correctamente.", "Aceptar");
            }
            catch (Exception ex)
            {
                MasterPage.MostrarModal("Error", "Error al subir la foto: " + ex.Message, "Aceptar");
            }
        }

        protected void btnEditarPerfil_Click(object sender, EventArgs e)
        {

            //Condición para redirigir según el rol
            switch (Session["Rol"])
            {
                case "Egresado":
                    Response.Redirect("../User/EditProfile.aspx");
                    break;
                case "Empresa":
                    Response.Redirect("../User/EditCompanyProfile.aspx");
                    break;
                default:
                    // Muestras un modal, mensaje, o lo que quieras
                    MasterPage.MostrarModal("Atención", "Debes completar tu información antes de editar el perfil.", "Aceptar");
                    break;
            }
        }

    }
}
