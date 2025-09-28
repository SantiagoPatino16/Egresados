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
        private PublicationsService _publicationsService;
        private CommentsService _commentsService;
        private int idCategoria = 1; // Variable para almacenar la categoría seleccionada
        private CategoriesService _categoriesService;

        protected void Page_Load(object sender, EventArgs e)
        {
            _publicationsService = new PublicationsService();
            _commentsService = new CommentsService();
            _categoriesService = new CategoriesService();

            if (!IsPostBack)
            {
                CargarCategorias();
                CargarPublicaciones();
            }
        }
        //Listar publicaciones con un párametro de categoría
        private void CargarPublicaciones(int? idCategoria = null)
        {
            var publicaciones = _publicationsService.ObtenerPublicacionesConComentarios(idCategoria);

            rptPublicaciones.DataSource = publicaciones;
            rptPublicaciones.DataBind();
        }

        // Cargar categorías para mostrar en el repeater
        private void CargarCategorias()
        {
            var categorias = _categoriesService.ListarTodas();

            rptCategorias.DataSource = categorias;
            rptCategorias.DataBind();
        }





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

        protected void btnVolverPublicaciones_Click(object sender, EventArgs e)
        {
            pnlComentariosPublicacion.Visible = false;
            rptPublicaciones.Visible = true;
            CargarPublicaciones();
        }

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

        protected void btnVolverComentarios_Click(object sender, EventArgs e)
        {
            pnlRespuestasComentario.Visible = false;
            pnlComentariosPublicacion.Visible = true;
        }

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
        // Filtrar publicaciones por categoría al hacer clic en el link de categoría
        protected void lnkCategoria_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            int idCategoria = Convert.ToInt32(lnk.CommandArgument);
            this.idCategoria = idCategoria; // Guardar la categoría seleccionada
            // Recargar publicaciones filtradas por esta categoría
            CargarPublicaciones(idCategoria);
        }

        //obtener el id de la categoría seleccionada
        protected void rptCategorias_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "SelectCategoria")
            {
                int idCategoria = Convert.ToInt32(e.CommandArgument);
                hfSelectedCategoria.Value = idCategoria.ToString(); // persistir
                CargarPublicaciones(idCategoria);
            }
        }


        // Métodos auxiliares - Debes implementarlos según tu sistema de autenticación
        private int ObtenerIdUsuarioActual()
        {
            //Guarda el id de usuario en la sesión al iniciar sesión (form login)
            var idUsuario = Session["IdUsuario"];
            return idUsuario != null ? Convert.ToInt32(idUsuario) : 0;
        }

        private string ObtenerNombreUsuarioActual()
        {
            //Guarda el nombre de usuario en la sesión al iniciar sesión (form login)
            var nombreUsuario = Session["NombreCompleto"] as string;
            return !string.IsNullOrEmpty(nombreUsuario) ? nombreUsuario : "Usuario desconocido";
        }

        private void MostrarModalError()
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "mostrarModalError", "$('#modalError').modal('show');", true);
            lblMensajeError.Text = "Ocurrió un error al procesar la solicitud. Por favor, inténtalo nuevamente más tarde.";
        }

        protected void rptPublicaciones_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Lógica adicional si es necesaria al cargar cada item
        }

        protected void rptComentariosPrincipales_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Lógica adicional si es necesaria al cargar cada comentario
        }
    }
}