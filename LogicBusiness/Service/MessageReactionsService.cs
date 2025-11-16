using Common.Attributes;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicBusiness.Service
{
    public class MessageReactionsService
    {
        private readonly MessageReactionsRepository _repo;

        public MessageReactionsService()
        {
            _repo = new MessageReactionsRepository();
        }

        public AttributesMessageReactions AgregarReaccion(int idMensaje, int idUsuario, int tipoReaccion)
        {
            if (tipoReaccion <= 0)
                throw new ArgumentException("Tipo de reacción inválido");

            // Si ya existe, la reemplazas
            _repo.QuitarReaccion(idMensaje, idUsuario);

            return _repo.AgregarReaccion(idMensaje, idUsuario, tipoReaccion);
        }

        public bool QuitarReaccion(int idMensaje, int idUsuario)
        {
            return _repo.QuitarReaccion(idMensaje, idUsuario);
        }

        public List<AttributesMessageReactions> ObtenerReacciones(int idMensaje)
        {
            return _repo.ObtenerReacciones(idMensaje);
        }
    }

}
