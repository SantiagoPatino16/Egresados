using Common.Attributes;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicBusiness.Service
{
    public class MessagesService
    {
        private readonly MessagesRepository _messagesRepository;

        public MessagesService()
        {
            _messagesRepository = new MessagesRepository();
        }

        public int EnviarMensaje(int emisorId, int receptorId, string contenido)
        {
            var msg = new AttributesMessages
            {
                IdEmisor = emisorId,
                IdReceptor = receptorId,
                Contenido = contenido,
                FechaEnvio = DateTime.Now,
                Leido = false
            };

            return _messagesRepository.InsertarMensaje(msg);
        }
        public List<AttributesMessages> ObtenerConversacion(int idUsuario1, int idUsuario2)
        {
            return _messagesRepository.ObtenerMensajes(idUsuario1, idUsuario2);
        }
        public bool EditarMensaje(int idMensaje, string nuevoContenido)
        {
            return _messagesRepository.EditarMensaje(idMensaje, nuevoContenido);
        }
        public bool EliminarMensaje(int idMensaje, int idUsuario)
        {
            return _messagesRepository.EliminarMensaje(idMensaje, idUsuario);
        }
        public List<AttributesMessages> ObtenerUltimosMensajes(int idUsuario)
        {
            return _messagesRepository.ObtenerUltimosMensajes(idUsuario);
        }
        public void MarcarMensajeComoLeido(int idMensaje)
        {
            _messagesRepository.MarcarMensajeComoLeido(idMensaje);
        }
        public int ContarMensajesNoLeidos(int idUsuario)
        {
            return _messagesRepository.ContarMensajesNoLeidos(idUsuario);
        }

    }
}
