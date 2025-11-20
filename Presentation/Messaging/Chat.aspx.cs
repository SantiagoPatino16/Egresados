using System;
using System.Web.UI;
using LogicBusiness.Service;

namespace Presentation.Messaging
{
    public partial class Chat : Page
    {
        private readonly MessagesService _messagesService;
        private readonly MessageAttachmentsService _attachmentsService;
        private readonly MessageReactionsService _reactionsService;

        public Chat()
        {
            _messagesService = new MessagesService();
            _attachmentsService = new MessageAttachmentsService();
            _reactionsService = new MessageReactionsService();
        }

        MainPage MasterPage
        {
            get { return (MainPage)this.Master; }
        }
        // ----------------- Propiedades para el markup -----------------
        protected int UsuarioActual
        {
            get
            {
                if (Session["IdUsuario"] != null)
                    return Convert.ToInt32(Session["IdUsuario"]);
                MasterPage.MostrarModal("Error", "Usuario no logueado.");
                return 0;
            }
        }

        protected int UsuarioDestino
        {
            get
            {
                if (Request.QueryString["usuarioId"] != null)
                    return Convert.ToInt32(Request.QueryString["usuarioId"]);
                MasterPage.MostrarModal("Error", "Usuario destino no especificado.");
                return 0;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarMensajes();
            }
        }

        private void CargarMensajes()
        {
            var mensajes = _messagesService.ObtenerConversacion(UsuarioActual, UsuarioDestino);
            foreach (var msg in mensajes)
                msg.Adjuntos = _attachmentsService.ObtenerAdjuntos(msg.IdMensaje);

            rptMensajes.DataSource = mensajes;
            rptMensajes.DataBind();
        }
    }
}

