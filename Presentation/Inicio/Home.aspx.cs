using LogicBusiness.Service;
using System;
using System.Web.UI;

namespace Presentation.Inicio
{
    public partial class Home : System.Web.UI.Page
    {
        private readonly EventsService _eventsService = new EventsService();
        private readonly UserService _userService = new UserService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verificar si hay sesión activa
                if (Session["IdUsuario"] == null)
                {
                    Response.Redirect("~/Start/Login.aspx");
                    return;
                }

                CargarDatosUsuario();
                CargarEventos();
                CargarSugerencias();
            }
        }

        private void CargarDatosUsuario()
        {
            try
            {
                int idUsuario = Convert.ToInt32(Session["IdUsuario"]);
                var usuario = _userService.ObtenerUsuarioPorId(idUsuario);

                if (usuario != null)
                {
                    // Nombre completo
                    lblNombreUsuario.Text = usuario.Nombre;

                    // Rol y descripción
                    string rol = usuario.Rol ?? "Usuario";
                    string descripcion = "";

                    if (rol.Equals("Egresado", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!string.IsNullOrEmpty(usuario.ProgramaAcademico))
                        {
                            descripcion = $"Egresado de {usuario.ProgramaAcademico}";
                            if (usuario.AnioGraduacion > 0)
                            {
                                descripcion += $" - {usuario.AnioGraduacion}";
                            }
                        }
                        else
                        {
                            descripcion = "Egresado de Institución Universitaria Pascual Bravo";
                        }
                    }
                    else if (rol.Equals("Estudiante", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!string.IsNullOrEmpty(usuario.ProgramaAcademico))
                        {
                            descripcion = $"Estudiante de {usuario.ProgramaAcademico}";
                        }
                        else
                        {
                            descripcion = "Estudiante de Institución Universitaria Pascual Bravo";
                        }
                    }
                    else if (rol.Equals("Empresa", StringComparison.OrdinalIgnoreCase))
                    {
                        descripcion = usuario.SectorIndustria ?? "Empresa";
                    }
                    else
                    {
                        descripcion = rol;
                    }

                    lblDescripcion.Text = descripcion;

                    // Ciudad
                    string ciudad = "";
                    if (rol.Equals("Empresa", StringComparison.OrdinalIgnoreCase))
                    {
                        ciudad = usuario.CiudadEmpresa ?? usuario.UbicacionEmpresa ?? "";
                    }
                    else
                    {
                        ciudad = usuario.CiudadResidencia ?? "";
                    }

                    if (!string.IsNullOrEmpty(ciudad))
                    {
                        lblCiudad.Text = ciudad;
                        lblCiudad.Visible = true;
                    }
                    else
                    {
                        lblCiudad.Visible = false;
                    }

                    // Foto de perfil
                    if (!string.IsNullOrEmpty(usuario.FotoPerfil))
                    {
                        imgAvatar.ImageUrl = "~/" + usuario.FotoPerfil;
                        imgAvatar.Visible = true;
                        avatarPlaceholder.Visible = false;
                    }
                    else
                    {
                        imgAvatar.Visible = false;
                        avatarPlaceholder.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblNombreUsuario.Text = Session["NombreCompleto"]?.ToString() ?? "Usuario";
                lblDescripcion.Text = "Institución Universitaria Pascual Bravo";
            }
        }

        private void CargarEventos()
        {
            try
            {
                // Obtener próximos eventos (solo los que no han finalizado)
                var eventos = _eventsService.ObtenerProximos();

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
                lblSinEventos.Text = "Error al cargar eventos: " + ex.Message;
                lblSinEventos.Visible = true;
            }
        }

        private void CargarSugerencias()
        {
            try
            {
                int idUsuario = Convert.ToInt32(Session["IdUsuario"]);
                var sugerencias = _userService.ObtenerUsuariosAleatorios(idUsuario, 4);

                if (sugerencias != null && sugerencias.Count > 0)
                {
                    rptSugerencias.DataSource = sugerencias;
                    rptSugerencias.DataBind();
                }
            }
            catch (Exception ex)
            {
                // Si hay error, simplemente no mostramos sugerencias
            }
        }

        // Método helper para determinar el estado del evento
        protected string ObtenerEstadoEvento(object fechaInicio, object fechaFin)
        {
            DateTime inicio = Convert.ToDateTime(fechaInicio);
            DateTime? fin = fechaFin != null ? Convert.ToDateTime(fechaFin) : (DateTime?)null;
            DateTime ahora = DateTime.Now;

            if (inicio > ahora)
            {
                // Evento futuro
                TimeSpan diferencia = inicio - ahora;
                if (diferencia.TotalDays < 1)
                {
                    return "Hoy";
                }
                else if (diferencia.TotalDays < 2)
                {
                    return "Mañana";
                }
                else if (diferencia.TotalDays < 7)
                {
                    return "Esta semana";
                }
                else
                {
                    return "Próximo Evento";
                }
            }
            else if (fin.HasValue && fin.Value >= ahora)
            {
                return "En curso";
            }
            else
            {
                return "Finalizado";
            }
        }

        // Método helper para obtener el color del badge según el estado
        protected string ObtenerColorEstado(object fechaInicio, object fechaFin)
        {
            string estado = ObtenerEstadoEvento(fechaInicio, fechaFin);
            
            switch (estado)
            {
                case "Hoy":
                case "Mañana":
                    return "danger"; // Rojo para urgente
                case "Esta semana":
                    return "warning"; // Amarillo para próximo
                case "En curso":
                    return "success"; // Verde para en curso
                default:
                    return "primary"; // Azul para futuro
            }
        }

        // Método helper para obtener la imagen del evento
        protected string ObtenerImagenEvento(object rutaImagen, object titulo)
        {
            string ruta = rutaImagen?.ToString();
            string tituloEvento = titulo?.ToString() ?? "Evento";

            if (!string.IsNullOrEmpty(ruta))
            {
                // Limpiar la ruta y asegurarse de que sea válida
                string rutaLimpia = ruta.Trim();
                
                // Si la ruta ya comienza con ~/ o ../ o /, usarla directamente
                if (rutaLimpia.StartsWith("~/") || rutaLimpia.StartsWith("../") || rutaLimpia.StartsWith("/"))
                {
                    return $"<img src='{ResolveUrl(rutaLimpia)}' class='event-image' alt='{tituloEvento}' />";
                }
                else
                {
                    // Si no tiene prefijo, agregar ~/
                    return $"<img src='{ResolveUrl("~/" + rutaLimpia)}' class='event-image' alt='{tituloEvento}' />";
                }
            }
            else
            {
                return "<div class='img-placeholder'><i class='bi bi-calendar-event'></i></div>";
            }
        }

        // Método helper para obtener el avatar del usuario
        protected string ObtenerAvatarUsuario(object fotoPerfil)
        {
            string foto = fotoPerfil?.ToString();

            if (!string.IsNullOrEmpty(foto))
            {
                // Limpiar la ruta y asegurarse de que sea válida
                string fotoLimpia = foto.Trim();
                
                // Si la ruta ya comienza con ~/ o ../ o /, usarla directamente
                if (fotoLimpia.StartsWith("~/") || fotoLimpia.StartsWith("../") || fotoLimpia.StartsWith("/"))
                {
                    return $"<img src='{ResolveUrl(fotoLimpia)}' class='avatar' />";
                }
                else
                {
                    // Si no tiene prefijo, agregar ~/
                    return $"<img src='{ResolveUrl("~/" + fotoLimpia)}' class='avatar' />";
                }
            }
            else
            {
                return "<span class='avatar'></span>";
            }
        }
    }
}
