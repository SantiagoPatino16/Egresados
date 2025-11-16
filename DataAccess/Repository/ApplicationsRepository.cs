using Common.Attributes;
using DataAccess.ConnectionDB;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repository
{
    public class ApplicationsRepository
    {
        private readonly RSContext _context;

        public ApplicationsRepository()
        {
            _context = new RSContext();
        }

        // Listar todas las postulaciones
        public List<AttributesApplications> ListarTodas()
        {
            return _context.Postulaciones.ToList();
        }

        // Obtener postulacion por Id
        public AttributesApplications ObtenerPorId(int id)
        {
            return _context.Postulaciones.FirstOrDefault(p => p.IdPostulacion == id);
        }

        // Agregar nueva postulacion
        public void Agregar(AttributesApplications postulacion)
        {
            _context.Postulaciones.Add(postulacion);
            _context.SaveChanges();
        }

        // Eliminar postulacion
        public void Eliminar(int id)
        {
            var postulacion = _context.Postulaciones.Find(id);
            if (postulacion != null)
            {
                _context.Postulaciones.Remove(postulacion);
                _context.SaveChanges();
            }
        }

        // Actualizar postulacion
        public void Actualizar(AttributesApplications postulacion)
        {
            var existente = _context.Postulaciones.Find(postulacion.IdPostulacion);
            if (existente != null)
            {
                existente.IdOferta = postulacion.IdOferta;
                existente.IdEgresado = postulacion.IdEgresado;
                existente.FechaPostulacion = postulacion.FechaPostulacion;
                existente.EstadoFinal = postulacion.EstadoFinal;
                existente.Mensaje = postulacion.Mensaje;
                existente.CvUrl = postulacion.CvUrl;

                _context.SaveChanges();
            }
        }
    }
}
