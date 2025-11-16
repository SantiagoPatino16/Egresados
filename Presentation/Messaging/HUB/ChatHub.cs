using Common.Attributes;
using LogicBusiness.Service;
using Microsoft.AspNet.SignalR;
using System;
using System.Threading.Tasks;

namespace Presentation.Messaging.HUB
{
    public class ChatHub : Hub
    {
        private readonly MessagesService _messagesService;
        private readonly MessageReactionsService _reactionsService;
        private readonly MessageAttachmentsService _attachmentsService;

        public ChatHub()
        {
            _messagesService = new MessagesService();
            _reactionsService = new MessageReactionsService();
            _attachmentsService = new MessageAttachmentsService();
        }

        //  MENSAJES 

        public async Task EnviarMensaje(int emisor, int receptor, string contenido)
        {
            int idMensaje = _messagesService.EnviarMensaje(emisor, receptor, contenido);

            // Receptor
            await Clients.Group($"user_{receptor}")
                .mensajeRecibido(idMensaje, emisor, contenido, DateTime.Now);

            // Emisor (opcional si quieres mostrarlo instantáneamente en su chat)
            await Clients.Group($"user_{emisor}")
                .mensajeEnviado(idMensaje, contenido, DateTime.Now);
        }


        public async Task EditarMensaje(int idMensaje, int emisor, int receptor, string nuevoContenido)
        {
            _messagesService.EditarMensaje(idMensaje, nuevoContenido);

            await Clients.Group($"user_{receptor}")
                .mensajeEditado(idMensaje, nuevoContenido);

            await Clients.Group($"user_{emisor}")
                .mensajeEditado(idMensaje, nuevoContenido);
        }


        public async Task EliminarMensaje(int idMensaje, int idUsuario)
        {
            _messagesService.EliminarMensaje(idMensaje, idUsuario);

            await Clients.All.mensajeEliminado(idMensaje);
        }

        public async Task MarcarMensajeLeido(int idMensaje, int emisor)
        {
            _messagesService.MarcarMensajeComoLeido(idMensaje);

            await Clients.Group($"user_{emisor}")
                .mensajeLeido(idMensaje, DateTime.Now);
        }


        //  ADJUNTOS 

        public async Task EnviarAdjunto(int idMensaje, int emisor, int receptor, string rutaArchivo, string mime)
        {
            _attachmentsService.InsertarAdjunto(new AttributesMessageAttachments
            {
                IdMensaje = idMensaje,
                RutaArchivo = rutaArchivo,
                TipoMime = mime,
                FechaAdjunto = DateTime.Now
            });

            await Clients.Group($"user_{receptor}")
                .adjuntoRecibido(idMensaje, rutaArchivo, mime);

            await Clients.Group($"user_{emisor}")
                .adjuntoRecibido(idMensaje, rutaArchivo, mime);
        }


        //  REACCIONES 

        public async Task AgregarReaccion(int idMensaje, int idUsuario, int receptor, int tipo)
        {
            _reactionsService.AgregarReaccion(idMensaje, idUsuario, tipo);

            await Clients.Group($"user_{receptor}")
                .reaccionAgregada(idMensaje, idUsuario, tipo);

            await Clients.Group($"user_{idUsuario}")
                .reaccionAgregada(idMensaje, idUsuario, tipo);
        }


        public async Task QuitarReaccion(int idMensaje, int idUsuario)
        {
            _reactionsService.QuitarReaccion(idMensaje, idUsuario);

            await Clients.All.reaccionQuitada(idMensaje, idUsuario);
        }

        // CONEXIÓN 

        public override Task OnConnected()
        {
            string userId = Context.QueryString["userId"];

            if (string.IsNullOrWhiteSpace(userId))
                throw new HubException("userId requerido");

            Groups.Add(Context.ConnectionId, $"user_{userId}");

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string userId = Context.QueryString["userId"];

            if (!string.IsNullOrWhiteSpace(userId))
                Groups.Remove(Context.ConnectionId, $"user_{userId}");

            return base.OnDisconnected(stopCalled);
        }
    }
}
