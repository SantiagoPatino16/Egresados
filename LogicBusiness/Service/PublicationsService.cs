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
        private readonly PublicationsRepository _publicService;

        public PublicationsService()
        {
            _publicService = new PublicationsRepository();
        }


        // Listar todas las publicaciones con sus respectivos comentarios
        public List<AttributesPublications> ObtenerPublicacionesConComentarios(int? idCategoria = null)
        {
            return _publicService.ObtenerPublicacionesConComentarios(idCategoria);
        }

        // Agregar nueva publicación
        public void Crear(AttributesPublications publicacion)
        {
            if (string.IsNullOrWhiteSpace(publicacion.Contenido))
            {
                throw new ArgumentException("El contenido es obligatorio.");
            }

            publicacion.Fecha = DateTime.Now;
            _publicService.Agregar(publicacion);
        }

        // Obtener publicación por Id
        public AttributesPublications Obtener(int id)
        {
            return _publicService.ObtenerPorId(id);
        }

        // Eliminar publicación
        public void Eliminar(int id)
        {
            _publicService.Eliminar(id);
        }
    }
}
