using Common.Attributes;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicBusiness.Service
{
    public class CommentsService
    {
        private readonly CommentsRepository _repo;

        public CommentsService()
        {
            _repo = new CommentsRepository();
        }

        // Listar comentarios de una publicación
        public List<AttributesComments> ListarPorPublicacion(int idPublicacion)
        {
            return _repo.ListarPorPublicacion(idPublicacion);
        }
        //Listar comentarios hijos de un comentario padre
        public List<AttributesComments> ListarRespuestas(int idComentarioPadre)
        {
            return _repo.ListarRespuestas(idComentarioPadre);
        }
        // Agregar comentario
        public void Crear(AttributesComments comentario)
        {
            if (string.IsNullOrWhiteSpace(comentario.Contenido))
            {
                throw new ArgumentException("El contenido del comentario es obligatorio.");
            }

            comentario.Fecha = DateTime.Now;
            _repo.Agregar(comentario);
        }

        // Obtener comentario por Id
        public AttributesComments Obtener(int id)
        {
            return _repo.ObtenerPorId(id);
        }

        // Eliminar comentario
        public void Eliminar(int id)
        {
            _repo.Eliminar(id);
        }
        // Listar comentarios por publicación
        public List<AttributesComments> ObtenerComentariosPorPublicacion(int idPublicacion)
        {
            return _repo.ListarPorPublicacion(idPublicacion);
        }
        public List<AttributesComments> ObtenerComentariosJerarquicos(int idPublicacion)
        {
            var comentarios = _repo.ListarPorPublicacion(idPublicacion);

            foreach (var c in comentarios)
                c.Respuestas = comentarios.Where(r => r.IdComentarioPadre == c.IdComentario).ToList();

            return comentarios.Where(c => c.IdComentarioPadre == null).ToList();
        }

    }
}
