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
        public int InsertarAdjunto(int idEmisor, int idReceptor, string rutaArchivo, string mimeType)
        {
            if (idEmisor <= 0) throw new ArgumentException("IdEmisor inválido");
            if (idReceptor <= 0) throw new ArgumentException("IdReceptor inválido");
            if (string.IsNullOrWhiteSpace(rutaArchivo)) throw new ArgumentException("RutaArchivo requerida");
            if (string.IsNullOrWhiteSpace(mimeType)) throw new ArgumentException("TipoMime requerido");

            return _messageAttachmentsRepository.InsertarAdjunto(
                idEmisor,
                idReceptor,
                rutaArchivo,
                mimeType
            );
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
