using Common.Attributes;
using DataAccess.ConnectionDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MessageAttachmentsRepositoy
    {
        public int InsertarAdjunto(AttributesMessageAttachments adjunto)
        {
            using (var context = new RSContext())
            {
                context.Adjuntos.Add(adjunto);
                context.SaveChanges();
                return adjunto.IdAdjunto;
            }
        }
        public List<AttributesMessageAttachments> ObtenerAdjuntos(int idMensaje)
        {
            using (var context = new RSContext())
            {
                return context.Adjuntos
                    .Where(a => a.IdMensaje == idMensaje)
                    .ToList();
            }
        }
        public bool EliminarAdjunto(int idAdjunto)
        {
            using (var context = new RSContext())
            {
                var adjunto = context.Adjuntos.FirstOrDefault(a => a.IdAdjunto == idAdjunto);
                if (adjunto != null)
                {
                    context.Adjuntos.Remove(adjunto);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }
        public bool TieneAdjuntos(int idMensaje)
        {
            using (var context = new RSContext())
            {
                return context.Adjuntos.Any(a => a.IdMensaje == idMensaje);
            }
        }
    }

}
