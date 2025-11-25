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

        // Helper para obtener userId del contexto (querystring)
        private int GetCallerUserId()
        {
            string userIdStr = Context.QueryString["userId"];
            if (string.IsNullOrWhiteSpace(userIdStr) || !int.TryParse(userIdStr, out int userId))
                throw new HubException("userId requerido en la conexión.");
            return userId;
        }

        //  MENSAJES 

        public async Task EnviarMensaje(int emisor, int receptor, string contenido)
        {
            int caller = GetCallerUserId();
            if (caller != emisor)
                throw new HubException("No autorizado: el emisor no coincide con el usuario conectado.");

            try
            {
                int idMensaje = _messagesService.EnviarMensaje(emisor, receptor, contenido);

                // Receptor
                await Clients.Group($"user_{receptor}")
                    .mensajeRecibido(idMensaje, emisor, contenido, DateTime.Now.ToString("s"));

                // Emisor (mostrarlo instantáneamente en su chat)
                await Clients.Group($"user_{emisor}")
                    .mensajeEnviado(idMensaje, contenido, DateTime.Now.ToString("s"));
            }
            catch (Exception ex)
            {
                throw new HubException("Error al enviar mensaje: " + ex.Message);
            }
        }


        public async Task EditarMensaje(int idMensaje, int emisor, int receptor, string nuevoContenido)
        {
            int caller = GetCallerUserId();
            if (caller != emisor)
                throw new HubException("No autorizado: solo el emisor puede editar su mensaje.");

            try
            {
                _messagesService.EditarMensaje(idMensaje, nuevoContenido);

                await Clients.Group($"user_{receptor}")
                    .mensajeEditado(idMensaje, nuevoContenido);

                await Clients.Group($"user_{emisor}")
                    .mensajeEditado(idMensaje, nuevoContenido);
            }
            catch (Exception ex)
            {
                throw new HubException("Error al editar mensaje: " + ex.Message);
            }
        }


        public async Task EliminarMensaje(int idMensaje, int idUsuario)
        {
            int caller = GetCallerUserId();
            if (caller != idUsuario)
                throw new HubException("No autorizado: solo el usuario propietario puede eliminar.");

            try
            {
                _messagesService.EliminarMensaje(idMensaje, idUsuario);

                await Clients.All.mensajeEliminado(idMensaje);
            }
            catch (Exception ex)
            {
                throw new HubException("Error al eliminar mensaje: " + ex.Message);
            }
        }

        public async Task MarcarMensajeLeido(int idMensaje, int emisor)
        {
            int caller = GetCallerUserId();
            // cualquiera puede reportar que leyó (se asume que la UI controla cuándo hacerlo),
            // pero podrías validar que el caller sea el receptor en tu lógica real.
            try
            {
                _messagesService.MarcarMensajeComoLeido(idMensaje);

                await Clients.Group($"user_{emisor}")
                    .mensajeLeido(idMensaje, DateTime.Now.ToString("s"));
            }
            catch (Exception ex)
            {
                throw new HubException("Error al marcar como leído: " + ex.Message);
            }
        }


        //  ADJUNTOS 

        public async Task EnviarAdjunto(int idMensaje, int emisor, int receptor, string rutaArchivo, string mime)
        {
            int caller = GetCallerUserId();
            if (caller != emisor)
                throw new HubException("No autorizado: el emisor no coincide con el usuario conectado.");

            try
            {
                await Clients.Group($"user_{receptor}")
                    .adjuntoRecibido(idMensaje, rutaArchivo, mime);

                await Clients.Group($"user_{emisor}")
                    .adjuntoRecibido(idMensaje, rutaArchivo, mime);
            }
            catch (Exception ex)
            {
                throw new HubException("Error al enviar adjunto: " + ex.Message);
            }
        }


        //  REACCIONES 

        public async Task AgregarReaccion(int idMensaje, int idUsuario, int receptor, int tipo)
        {
            int caller = GetCallerUserId();
            if (caller != idUsuario)
                throw new HubException("No autorizado: la reacción debe provenir del usuario conectado.");

            try
            {
                // Esto actualizará o insertará
                var reaccion = _reactionsService.AgregarReaccion(idMensaje, idUsuario, tipo);

                // Notificar al receptor y al emisor (para 1-a-1)
                await Clients.Group($"user_{receptor}")
                    .reaccionAgregada(idMensaje, idUsuario, (int)reaccion.TipoReaccion);

                await Clients.Group($"user_{idUsuario}")
                    .reaccionAgregada(idMensaje, idUsuario, (int)reaccion.TipoReaccion);
            }
            catch (Exception ex)
            {
                throw new HubException("Error al agregar reacción: " + ex.Message);
            }
        }



        public async Task QuitarReaccion(int idMensaje, int idUsuario)
        {
            int caller = GetCallerUserId();
            if (caller != idUsuario)
                throw new HubException("No autorizado: la acción debe provenir del usuario conectado.");

            try
            {
                _reactionsService.QuitarReaccion(idMensaje, idUsuario);

                await Clients.All.reaccionQuitada(idMensaje, idUsuario);
            }
            catch (Exception ex)
            {
                throw new HubException("Error al quitar reacción: " + ex.Message);
            }
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
