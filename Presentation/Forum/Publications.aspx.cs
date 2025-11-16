using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Attributes;
using LogicBusiness.Service;

namespace Presentation.Forum
{
    public partial class Publications : System.Web.UI.Page
    {
        #region Variables y Servicios
        private PublicationsService _publicationsService;
        private CommentsService _commentsService;
        private CategoriesService _categoriesService;
        private UserService _userService;
        private int idCategoria = 1; // Variable para almacenar la categoría seleccionada
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            _publicationsService = new PublicationsService();
            _commentsService = new CommentsService();
            _categoriesService = new CategoriesService();
            _userService = new UserService();

            if (!IsPostBack)
            {
                CargarCategorias();
                CargarPublicaciones();
            }
        }
        #endregion

        #region Métodos de Carga de Datos
        /// <summary>
        /// Listar publicaciones con un parámetro de categoría
        /// </summary>
        private void CargarPublicaciones(int? idCategoria = null)
        {
            var publicaciones = _publicationsService.ObtenerPublicacionesConComentarios(idCategoria);

            rptPublicaciones.DataSource = publicaciones;
            rptPublicaciones.DataBind();
        }

        /// <summary>
        /// Cargar categorías para mostrar en el repeater
        /// </summary>
        private void CargarCategorias()
        {
            var categorias = _categoriesService.ListarTodas();

            rptCategorias.DataSource = categorias;
            rptCategorias.DataBind();
        }
        #endregion

        #region Eventos de Publicaciones
        protected void btnPublicar_Click(object sender, EventArgs e)
        {
            string contenido = txtContenido.Text.Trim();
            if (!string.IsNullOrEmpty(contenido))
            {
                try
                {
                    // Aquí deberías obtener el ID del usuario actual
                    int idUsuarioActual = ObtenerIdUsuarioActual(); // Implementa este método según tu sistema de autenticación
                    //Recuperar la categoría seleccionada
                    int idCategoria = int.TryParse(hfSelectedCategoria.Value, out int selectedId) ? selectedId : 1;

                    var nuevaPublicacion = new AttributesPublications
                    {
                        IdUsuario = idUsuarioActual,
                        NombreUsuario = ObtenerNombreUsuarioActual(), // Implementa este método
                        Contenido = contenido,
                        Fecha = DateTime.Now,
                        IdCategoria = idCategoria
                    };

                    _publicationsService.Crear(nuevaPublicacion);
                    txtContenido.Text = "";
                    CargarPublicaciones();
                }
                catch (Exception ex)
                {
                    MostrarModalError(); // Mostrar modal de error creado en la parte inferior del código
                }
            }
        }

        protected void btnVerComentarios_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            int idPublicacion = Convert.ToInt32(button.CommandArgument);
            MostrarComentariosPublicacion(idPublicacion);
        }

        protected void btnVolverPublicaciones_Click(object sender, EventArgs e)
        {
            pnlComentariosPublicacion.Visible = false;
            rptPublicaciones.Visible = true;
            CargarPublicaciones();
        }
        #endregion

        #region Métodos de Vista - Publicaciones
        private void MostrarComentariosPublicacion(int idPublicacion)
        {
            try
            {
                var publicacion = _publicationsService.Obtener(idPublicacion);
                if (publicacion != null)
                {
                    lblNombreUsuarioPub.Text = publicacion.NombreUsuario;
                    lblContenidoPub.Text = publicacion.Contenido;
                    lblFechaPub.Text = publicacion.Fecha.ToString("dd/MM/yyyy HH:mm");

                    // Obtener foto de perfil del usuario
                    var usuario = _userService.ObtenerFotoPerfil(publicacion.IdUsuario);
                    string fotoPerfilUrl = usuario;

                    // Renderizar foto de perfil de la publicación
                    RenderizarFotoPerfil(phFotoPerfilPub, fotoPerfilUrl, publicacion.NombreUsuario, 50);

                    // Obtener comentarios jerárquicos
                    var comentariosJerarquicos = _commentsService.ObtenerComentariosJerarquicos(idPublicacion);
                    rptComentariosPrincipales.DataSource = comentariosJerarquicos;
                    rptComentariosPrincipales.DataBind();

                    // Guardar en ViewState
                    ViewState["IdPublicacionActual"] = idPublicacion;

                    // Mostrar panel de comentarios
                    pnlComentariosPublicacion.Visible = true;
                    rptPublicaciones.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MostrarModalError(); // Mostrar modal de error creado en la parte inferior del código
            }
        }
        #endregion

        #region Eventos de Comentarios
        protected void btnAgregarComentario_Click(object sender, EventArgs e)
        {
            string contenido = txtNuevoComentario.Text.Trim();
            if (!string.IsNullOrEmpty(contenido))
            {
                try
                {
                    int idPublicacion = Convert.ToInt32(ViewState["IdPublicacionActual"]);
                    int idUsuarioActual = ObtenerIdUsuarioActual();

                    var nuevoComentario = new AttributesComments
                    {
                        IdPublicacion = idPublicacion,
                        IdUsuario = idUsuarioActual,
                        NombreUsuario = ObtenerNombreUsuarioActual(),
                        Contenido = contenido,
                        Fecha = DateTime.Now,
                        IdComentarioPadre = null // Comentario principal
                    };

                    _commentsService.Crear(nuevoComentario);
                    txtNuevoComentario.Text = "";
                    MostrarComentariosPublicacion(idPublicacion); // Recargar comentarios
                }
                catch (Exception ex)
                {
                    MostrarModalError(); // Mostrar modal de error creado en la parte inferior del código
                }
            }
        }

        protected void btnVerRespuestas_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            int idComentario = Convert.ToInt32(button.CommandArgument);
            MostrarRespuestasComentario(idComentario);
        }

        protected void btnVolverComentarios_Click(object sender, EventArgs e)
        {
            pnlRespuestasComentario.Visible = false;
            pnlComentariosPublicacion.Visible = true;
        }
        #endregion

        #region Eventos de Respuestas
        protected void btnAgregarRespuesta_Click(object sender, EventArgs e)
        {
            string contenido = txtNuevaRespuesta.Text.Trim();
            if (!string.IsNullOrEmpty(contenido))
            {
                try
                {
                    int idComentarioPadre = Convert.ToInt32(ViewState["IdComentarioActual"]);
                    int idPublicacion = Convert.ToInt32(ViewState["IdPublicacionActual"]);
                    int idUsuarioActual = ObtenerIdUsuarioActual();

                    // Obtener el comentario padre para obtener su IdPublicacion si no está en ViewState
                    var comentarioPadre = _commentsService.Obtener(idComentarioPadre);

                    var nuevaRespuesta = new AttributesComments
                    {
                        IdPublicacion = comentarioPadre?.IdPublicacion ?? idPublicacion,
                        IdUsuario = idUsuarioActual,
                        NombreUsuario = ObtenerNombreUsuarioActual(),
                        Contenido = contenido,
                        Fecha = DateTime.Now,
                        IdComentarioPadre = idComentarioPadre
                    };

                    _commentsService.Crear(nuevaRespuesta);
                    txtNuevaRespuesta.Text = "";
                    MostrarRespuestasComentario(idComentarioPadre); // Recargar respuestas
                }
                catch (Exception ex)
                {
                    MostrarModalError(); // Mostrar modal de error creado en la parte inferior del código
                }
            }
        }
        #endregion

        #region Métodos de Vista - Respuestas
        private void MostrarRespuestasComentario(int idComentario)
        {
            try
            {
                var comentario = _commentsService.Obtener(idComentario);
                if (comentario != null)
                {
                    lblNombreUsuarioCom.Text = comentario.NombreUsuario;
                    lblContenidoCom.Text = comentario.Contenido;
                    lblFechaCom.Text = comentario.Fecha.ToString("dd/MM/yyyy HH:mm");

                    // Obtener foto de perfil del usuario
                    var usuario = _userService.ObtenerFotoPerfil(comentario.IdUsuario);
                    string fotoPerfilUrl = usuario;

                    // Renderizar foto de perfil del comentario
                    RenderizarFotoPerfil(phFotoPerfilComResp, fotoPerfilUrl, comentario.NombreUsuario, 50);

                    // Obtener respuestas
                    var respuestas = _commentsService.ListarRespuestas(idComentario);
                    rptRespuestas.DataSource = respuestas;
                    rptRespuestas.DataBind();

                    // Guardar en ViewState
                    ViewState["IdComentarioActual"] = idComentario;

                    // Mostrar panel de respuestas
                    pnlRespuestasComentario.Visible = true;
                    pnlComentariosPublicacion.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MostrarModalError(); // Mostrar modal de error creado en la parte inferior del código
            }
        }
        #endregion

        #region Eventos de Categorías
        /// <summary>
        /// Filtrar publicaciones por categoría al hacer clic en el link de categoría
        /// </summary>
        protected void lnkCategoria_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            int idCategoria = Convert.ToInt32(lnk.CommandArgument);
            this.idCategoria = idCategoria; // Guardar la categoría seleccionada
            // Recargar publicaciones filtradas por esta categoría
            CargarPublicaciones(idCategoria);
        }

        /// <summary>
        /// Obtener el id de la categoría seleccionada
        /// </summary>
        protected void rptCategorias_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "SelectCategoria")
            {
                int idCategoria = Convert.ToInt32(e.CommandArgument);
                hfSelectedCategoria.Value = idCategoria.ToString(); // persistir
                CargarPublicaciones(idCategoria);
            }
        }
        #endregion

        #region Eventos ItemDataBound de Repeaters
        protected void rptPublicaciones_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var publicacion = (AttributesPublications)e.Item.DataItem;
                var phFotoPerfil = (PlaceHolder)e.Item.FindControl("phFotoPerfil");

                // Obtener foto de perfil del usuario
                var usuario = _userService.ObtenerFotoPerfil(publicacion.IdUsuario);
                string fotoPerfilUrl = usuario;

                RenderizarFotoPerfil(phFotoPerfil, fotoPerfilUrl, publicacion.NombreUsuario, 50);
            }
        }

        protected void rptComentariosPrincipales_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var comentario = (AttributesComments)e.Item.DataItem;
                var phFotoPerfilCom = (PlaceHolder)e.Item.FindControl("phFotoPerfilCom");

                // Obtener foto de perfil del usuario
                var usuario = _userService.ObtenerFotoPerfil(comentario.IdUsuario);
                string fotoPerfilUrl = usuario;

                RenderizarFotoPerfil(phFotoPerfilCom, fotoPerfilUrl, comentario.NombreUsuario, 40);
            }
        }

        protected void rptRespuestas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var respuesta = (AttributesComments)e.Item.DataItem;
                var phFotoPerfilResp = (PlaceHolder)e.Item.FindControl("phFotoPerfilResp");

                // Obtener foto de perfil del usuario
                var usuario = _userService.ObtenerFotoPerfil(respuesta.IdUsuario);
                string fotoPerfilUrl = usuario;

                RenderizarFotoPerfil(phFotoPerfilResp, fotoPerfilUrl, respuesta.NombreUsuario, 35);
            }
        }
        #endregion

        #region Métodos de Renderizado de Fotos de Perfil
        /// <summary>
        /// Renderiza la foto de perfil o un avatar con iniciales si no hay foto
        /// </summary>
        private void RenderizarFotoPerfil(PlaceHolder placeholder, string fotoPerfilUrl, string nombreUsuario, int tamanio)
        {
            if (placeholder == null) return;

            placeholder.Controls.Clear();

            if (!string.IsNullOrEmpty(fotoPerfilUrl))
            {
                // Si tiene foto de perfil, mostrar la imagen
                var img = new System.Web.UI.WebControls.Image
                {
                    ImageUrl = fotoPerfilUrl,
                    CssClass = "rounded-circle border border-2",
                    Width = tamanio,
                    Height = tamanio,
                    AlternateText = nombreUsuario,
                    Style = { ["object-fit"] = "cover" }
                };
                placeholder.Controls.Add(img);
            }
            else
            {
                // Si no tiene foto, mostrar avatar con iniciales
                string iniciales = ObtenerIniciales(nombreUsuario);
                string colorFondo = ObtenerColorAvatar(nombreUsuario);

                var avatarDiv = new Literal
                {
                    Text = $@"<div class='rounded-circle border border-2 d-flex align-items-center justify-content-center text-white fw-bold' 
                                  style='width: {tamanio}px; height: {tamanio}px; background: {colorFondo}; font-size: {tamanio / 2.5}px;'>
                                  {iniciales}
                              </div>"
                };
                placeholder.Controls.Add(avatarDiv);
            }
        }

        /// <summary>
        /// Obtiene las iniciales del nombre del usuario (máximo 2 letras)
        /// </summary>
        private string ObtenerIniciales(string nombreCompleto)
        {
            if (string.IsNullOrEmpty(nombreCompleto))
                return "U";

            var palabras = nombreCompleto.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (palabras.Length >= 2)
            {
                // Si tiene nombre y apellido, tomar primera letra de cada uno
                return (palabras[0][0].ToString() + palabras[1][0].ToString()).ToUpper();
            }
            else if (palabras.Length == 1 && palabras[0].Length >= 2)
            {
                // Si solo tiene un nombre, tomar las primeras 2 letras
                return palabras[0].Substring(0, 2).ToUpper();
            }
            else if (palabras.Length == 1)
            {
                // Si solo tiene una letra
                return palabras[0][0].ToString().ToUpper();
            }

            return "U";
        }

        /// <summary>
        /// Genera un color consistente basado en el nombre del usuario
        /// </summary>
        private string ObtenerColorAvatar(string nombreUsuario)
        {
            // Lista de colores agradables para avatares
            var colores = new[]
            {
                "linear-gradient(135deg, #667eea 0%, #764ba2 100%)",
                "linear-gradient(135deg, #f093fb 0%, #f5576c 100%)",
                "linear-gradient(135deg, #4facfe 0%, #00f2fe 100%)",
                "linear-gradient(135deg, #43e97b 0%, #38f9d7 100%)",
                "linear-gradient(135deg, #fa709a 0%, #fee140 100%)",
                "linear-gradient(135deg, #30cfd0 0%, #330867 100%)",
                "linear-gradient(135deg, #a8edea 0%, #fed6e3 100%)",
                "linear-gradient(135deg, #ff9a9e 0%, #fecfef 100%)",
                "linear-gradient(135deg, #ffecd2 0%, #fcb69f 100%)",
                "linear-gradient(135deg, #ff6e7f 0%, #bfe9ff 100%)"
            };

            // Usar el hash del nombre para seleccionar un color consistente
            int hash = 0;
            if (!string.IsNullOrEmpty(nombreUsuario))
            {
                foreach (char c in nombreUsuario)
                {
                    hash = ((hash << 5) - hash) + c;
                }
            }

            int index = Math.Abs(hash) % colores.Length;
            return colores[index];
        }
        #endregion

        #region Métodos Auxiliares - Autenticación
        /// <summary>
        /// Guarda el id de usuario en la sesión al iniciar sesión (form login)
        /// </summary>
        private int ObtenerIdUsuarioActual()
        {
            var idUsuario = Session["IdUsuario"];
            return idUsuario != null ? Convert.ToInt32(idUsuario) : 0;
        }

        /// <summary>
        /// Guarda el nombre de usuario en la sesión al iniciar sesión (form login)
        /// </summary>
        private string ObtenerNombreUsuarioActual()
        {
            var nombreUsuario = Session["NombreCompleto"] as string;
            return !string.IsNullOrEmpty(nombreUsuario) ? nombreUsuario : "Usuario desconocido";
        }
        #endregion

        #region Métodos de UI - Modales y Mensajes
        private void MostrarModalError()
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "mostrarModalError", "$('#modalError').modal('show');", true);
            lblMensajeError.Text = "Ocurrió un error al procesar la solicitud. Por favor, inténtalo nuevamente más tarde.";
        }
        #endregion
    }
}