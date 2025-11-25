using Common.Attributes;
using Common.DTO;
using LogicBusiness.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;   // para [WebMethod]
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Presentation.Messaging
{
    public partial class Chat : Page
    {

        /* ==========================================
         * SERVICES
         * ========================================== */
        private readonly MessagesService _msgSvc = new MessagesService();
        private readonly MessageAttachmentsService _attSvc = new MessageAttachmentsService();
        private readonly MessageReactionsService _reactSvc = new MessageReactionsService();
        private readonly UserService _userSvc = new UserService();

        /* ==========================================
         * PROPIEDADES DE LECTURA RÁPIDA
         * ========================================== */
        private int UsuarioActual
        {
            get
            {
                if (Session["IdUsuario"] != null) return Convert.ToInt32(Session["IdUsuario"]);
                MasterPage.MostrarModal("Error", "Usuario no logueado.");
                return 0;
            }
        }
        MainPage MasterPage
        {
            get { return (MainPage)this.Master; }
        }

        private int UsuarioDestino
        {
            get
            {
                int? id = null;
                if (int.TryParse(Request.QueryString["usuarioId"], out int tmp)) id = tmp;
                if (!id.HasValue) MasterPage.MostrarModal("Error", "Destinatario no especificado.");
                return id.GetValueOrDefault();
            }
        }

        /* ==========================================
         * DATOS PARA EL MARKUP (NOMBRE DEL DESTINO)
         * ========================================== */
        public string NombreDestino
        {
            get
            {
                var usr = _userSvc.ObtenerUsuarioPorId(UsuarioDestino);
                return usr?.Nombre ?? "Usuario";
            }
        }

        /* ==========================================
         * PAGE LOAD
         * ========================================== */
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["IdUsuario"] == null)
                    Response.Redirect("../Start/Login.aspx");


                string rutaFoto = _userSvc.ObtenerFotoPerfil(UsuarioDestino);

                imgAvatar.Src = !string.IsNullOrEmpty(rutaFoto)
                                ? ResolveUrl(rutaFoto)
                                : ResolveUrl("../Resources/perfiles/default.png");

                var mensajes = _msgSvc.ObtenerConversacion(UsuarioActual, UsuarioDestino);
                rptHistorial.DataSource = mensajes;
                rptHistorial.DataBind();

                hfUsuarioActual.Value = UsuarioActual.ToString();
                hfUsuarioDestino.Value = UsuarioDestino.ToString();
            }
        }

        /* ==========================================
        * Repeater
        * ========================================== */

        protected void rptHistorial_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
                return;

            var mensaje = (AttributesMessages)e.Item.DataItem;

            var ltrAlign = (Literal)e.Item.FindControl("ltrAlign");
            var ltrStyle = (Literal)e.Item.FindControl("ltrStyle");
            var ltrLeido = (Literal)e.Item.FindControl("ltrLeido");

            if (mensaje.IdEmisor == UsuarioActual)
            {
                ltrAlign.Text = "justify-content-end";
                ltrStyle.Text = "msg-out max-w-75 shadow-sm";
            }
            else
            {
                ltrAlign.Text = "justify-content-start";
                ltrStyle.Text = "msg-in max-w-75 shadow-sm";
            }
        }





        /* ==========================================
         * WEB METHOD: HISTORIAL PARA JS
         * ========================================== */
        [WebMethod]
        public static List<MensajeChatDto> ObtenerHistorial(int usuarioDestino)
        {
            int usuarioActual = 0;
            if (HttpContext.Current.Session["IdUsuario"] != null)
                usuarioActual = Convert.ToInt32(HttpContext.Current.Session["IdUsuario"]);
            if (usuarioActual == 0) return new List<MensajeChatDto>();

            var msgSvc = new MessagesService();
            var attSvc = new MessageAttachmentsService();
            var reactSvc = new MessageReactionsService();

            // 1. Traemos ENTIDADES
            var mensajes = msgSvc.ObtenerConversacion(usuarioActual, usuarioDestino);

            // 2. Mapeo a DTO PLANO (sin proxies, sin navegaciones)
            var dto = mensajes.Select(m => new MensajeChatDto
            {
                IdMensaje = m.IdMensaje,
                IdEmisor = m.IdEmisor,
                Contenido = m.Contenido,
                FechaEnvio = m.FechaEnvio.ToString("s"),
                Leido = m.Leido,
                // 3. Listas SIN proxies
                Adjuntos = attSvc.ObtenerAdjuntos(m.IdMensaje)
                                   .Select(a => new AdjuntoDto
                                   {
                                       IdAdjunto = a.IdAdjunto,
                                       RutaArchivo = a.RutaArchivo,
                                       TipoMime = a.TipoMime
                                   }).ToList(),
                Reacciones = reactSvc.ObtenerReacciones(m.IdMensaje)
                                   .Select(r => new ReaccionDto
                                   {
                                       IdMensaje = r.IdMensaje,
                                       IdUsuario = r.IdUsuario,
                                       TipoReaccion = (int)r.TipoReaccion
                                   }).ToList()
            }).ToList();

            return dto;
        }

        private bool EsWebMethodCall()
        {
            return Request.Path.Contains(".aspx/");   // funciona siempre
        }


    }
}