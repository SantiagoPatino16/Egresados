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
        public int InsertarAdjunto(int idEmisor, int idReceptor, string rutaArchivo, string mimeType)
        {
            using (var context = new RSContext())
            {
                // === 1. Crear el mensaje "contenedor" ===
                var mensaje = new AttributesMessages
                {
                    IdEmisor = idEmisor,
                    IdReceptor = idReceptor,
                    Contenido = rutaArchivo,   // Puede ser la ruta o un texto como "[archivo adjunto]"
                    FechaEnvio = DateTime.Now,
                    EliminadoPorEmisor = false,
                    EliminadoPorReceptor = false,
                    Editado = false,
                    Leido = false
                };

                context.Mensajes.Add(mensaje);
                context.SaveChanges();

                // === 2. Crear el registro de adjunto ===
                var adjunto = new AttributesMessageAttachments
                {
                    IdMensaje = mensaje.IdMensaje,
                    RutaArchivo = rutaArchivo,
                    TipoMime = mimeType,
                    FechaAdjunto = DateTime.Now
                };

                context.Adjuntos.Add(adjunto);
                context.SaveChanges();

                // === 3. Devuelves el IdMensaje, no el IdAdjunto ===
                return mensaje.IdMensaje;
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
