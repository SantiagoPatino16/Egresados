using Common.Attributes;
using DataAccess.ConnectionDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MessagesRepository
    {
        public int InsertarMensaje(AttributesMessages mensaje)
        {
            using (var context = new RSContext())
            {
                context.Mensajes.Add(mensaje);
                context.SaveChanges();
                return mensaje.IdMensaje;
            }
        }
        public List<AttributesMessages> ObtenerMensajes(int idUsuario1, int idUsuario2)
        {
            using (var context = new RSContext())
            {
                return context.Mensajes
                    .Include("Emisor")
                    .Include("Receptor")
                    .Where(m =>
                        (m.IdEmisor == idUsuario1 && m.IdReceptor == idUsuario2 && !m.EliminadoPorEmisor) ||
                        (m.IdEmisor == idUsuario2 && m.IdReceptor == idUsuario1 && !m.EliminadoPorReceptor))
                    .OrderBy(m => m.FechaEnvio)
                    .ToList();
            }
        }
        public bool EditarMensaje(int idMensaje, string nuevoContenido)
        {
            using (var context = new RSContext())
            {
                var mensaje = context.Mensajes.FirstOrDefault(m => m.IdMensaje == idMensaje);
                if (mensaje != null)
                {
                    mensaje.Contenido = nuevoContenido;
                    mensaje.Editado = true;
                    mensaje.FechaEdicion = DateTime.Now;
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }
        public bool EliminarMensaje(int idMensaje, int idUsuario)
        {
            using (var context = new RSContext())
            {
                var mensaje = context.Mensajes.FirstOrDefault(m => m.IdMensaje == idMensaje);
                if (mensaje != null)
                {
                    if (mensaje.IdEmisor == idUsuario)
                    {
                        mensaje.EliminadoPorEmisor = true;
                    }
                    else if (mensaje.IdReceptor == idUsuario)
                    {
                        mensaje.EliminadoPorReceptor = true;
                    }
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        public List<AttributesMessages> ObtenerUltimosMensajes(int idUsuario)
        {
            using (var context = new RSContext())
            {
                var mensajes = context.Mensajes
                    .Where(m => (m.IdEmisor == idUsuario && !m.EliminadoPorEmisor) || (m.IdReceptor == idUsuario && !m.EliminadoPorReceptor))
                    .GroupBy(m => m.IdEmisor == idUsuario ? m.IdReceptor : m.IdEmisor)
                    .Select(g => g.OrderByDescending(m => m.FechaEnvio).FirstOrDefault())
                    .OrderByDescending(m => m.FechaEnvio)
                    .ToList();
                return mensajes;
            }
        }
        public void MarcarMensajeComoLeido(int idMensaje)
        {
            using (var context = new RSContext())
            {
                var mensaje = context.Mensajes.FirstOrDefault(m => m.IdMensaje == idMensaje);
                if (mensaje != null)
                {
                    mensaje.Leido = true;
                    context.SaveChanges();
                }
            }
        }
        public int ContarMensajesNoLeidos(int idUsuario)
        {
            using (var context = new RSContext())
            {
                var noLeidos = context.Mensajes
                    .Count(m => m.IdReceptor == idUsuario && !m.Leido && !m.EliminadoPorReceptor);
                return noLeidos;
            }
        }
    }
}
