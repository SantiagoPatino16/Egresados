using Common.Attributes;
using DataAccess.ConnectionDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MessageReactionsRepository
    {
        public AttributesMessageReactions AgregarReaccion(int idMensaje, int idUsuario, int tipoReaccion)
        {
            using (var context = new RSContext())
            {
                // Buscar si ya existe reacción del usuario en este mensaje
                var reaccion = context.Chats
                    .FirstOrDefault(r => r.IdMensaje == idMensaje && r.IdUsuario == idUsuario);

                if (reaccion != null)
                {
                    // Actualizar tipo de reacción
                    reaccion.TipoReaccion = (AttributesMessageReactions.ReactionsMessages)tipoReaccion;
                }
                else
                {
                    // Crear nueva reacción
                    reaccion = new AttributesMessageReactions
                    {
                        IdMensaje = idMensaje,
                        IdUsuario = idUsuario,
                        TipoReaccion = (AttributesMessageReactions.ReactionsMessages)tipoReaccion
                    };
                    context.Chats.Add(reaccion);
                }

                context.SaveChanges();
                return reaccion;
            }
        }

        public bool QuitarReaccion(int idMensaje, int idUsuario)
        {
            using (var context = new RSContext())
            {
                var reaccion = context.Chats.FirstOrDefault(r => r.IdMensaje == idMensaje && r.IdUsuario == idUsuario);
                if (reaccion != null)
                {
                    context.Chats.Remove(reaccion);
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        public List<AttributesMessageReactions> ObtenerReacciones(int idMensaje)
        {
            using (var context = new RSContext())
            {
                return context.Chats
                    .Where(r => r.IdMensaje == idMensaje)
                    .ToList();
            }
        }
    }
}
