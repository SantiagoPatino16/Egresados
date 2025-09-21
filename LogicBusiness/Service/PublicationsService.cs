using Common.Attributes;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicBusiness.Service
{
    public class PublicationsService
    {
        private readonly PublicationsRepository _repo;
        private readonly CommentsService _commentService;

        public PublicationsService()
        {
            _repo = new PublicationsRepository();
            _commentService = new CommentsService();
        }


        // Listar todas las publicaciones con sus respectivos comentarios
        public List<AttributesPublications> ObtenerPublicacionesConComentarios()
        {
            var publicaciones = _repo.Listar();

            foreach (var pub in publicaciones)
            {
                // Si no hay comentarios, la lista quedará vacía
                pub.Comentarios = _commentService.ObtenerComentariosPorPublicacion(pub.IdPublicacion) ?? new List<AttributesComments>();
            }

            return publicaciones;
        }

        // Agregar nueva publicación
        public void Crear(AttributesPublications publicacion)
        {
            if (string.IsNullOrWhiteSpace(publicacion.Contenido))
            {
                throw new ArgumentException("El contenido es obligatorio.");
            }

            publicacion.Fecha = DateTime.Now;
            _repo.Agregar(publicacion);
        }

        // Obtener publicación por Id
        public AttributesPublications Obtener(int id)
        {
            return _repo.ObtenerPorId(id);
        }

        // Eliminar publicación
        public void Eliminar(int id)
        {
            _repo.Eliminar(id);
        }
    }
}
