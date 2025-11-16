using Common.Attributes;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicBusiness.Service
{
    public class MessageAttachmentsService
    {
        private readonly MessageAttachmentsRepositoy _messageAttachmentsRepository;
        public MessageAttachmentsService()
        {
            _messageAttachmentsRepository = new MessageAttachmentsRepositoy();
        }
        public int InsertarAdjunto(AttributesMessageAttachments adjunto)
        {
            return _messageAttachmentsRepository.InsertarAdjunto(adjunto);
        }
        public List<AttributesMessageAttachments> ObtenerAdjuntos(int idMensaje)
        {
            return _messageAttachmentsRepository.ObtenerAdjuntos(idMensaje);
        }
        public bool EliminarAdjunto(int idAdjunto)
        {
            return _messageAttachmentsRepository.EliminarAdjunto(idAdjunto);
        }
        public bool TieneAdjuntos(int idMensaje)
        {
            return _messageAttachmentsRepository.TieneAdjuntos(idMensaje);
        }
    }
}
