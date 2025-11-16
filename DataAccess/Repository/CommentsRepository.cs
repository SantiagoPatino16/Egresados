using Common.Attributes;
using Common.Entities;
using DataAccess.ConnectionDB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repository
{
    public class CommentsRepository
    {
        private readonly RSContext _context;

        public CommentsRepository()
        {
            _context = new RSContext();
        }

        // Listar comentarios de una publicación
        public List<AttributesComments> ListarPorPublicacion(int idPublicacion)
        {
            return _context.Comentarios
                           .Where(c => c.IdPublicacion == idPublicacion)
                           .OrderBy(c => c.Fecha)
                           .ToList();
        }

        // Agregar comentario
        public void Agregar(AttributesComments comentario)
        {
            _context.Comentarios.Add(comentario);
            _context.SaveChanges();
        }

        // Obtener comentario por Id
        public AttributesComments ObtenerPorId(int id)
        {
            return _context.Comentarios.FirstOrDefault(c => c.IdComentario == id);
        }

        // Eliminar comentario
        public void Eliminar(int id)
        {
            var com = _context.Comentarios.Find(id);
            if (com != null)
            {
                _context.Comentarios.Remove(com);
                _context.SaveChanges();
            }
        }

        // Listar solo respuestas de un comentario
        public List<AttributesComments> ListarRespuestas(int idComentarioPadre)
        {
            return _context.Comentarios
                .Where(c => c.IdComentarioPadre == idComentarioPadre)
                .OrderBy(c => c.Fecha)
                .ToList();
        }
    }
}
