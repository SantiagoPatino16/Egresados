using Common.Attributes;
using DataAccess.ConnectionDB;
using System.Data.Entity; // <-- EF6
using System.Linq;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public class StatusApplicationRepository
    {
        private readonly RSContext _context;

        public StatusApplicationRepository()
        {
            _context = new RSContext();
        }

        // Listar todos los estados
        public List<AttributesStatusApplication> ListarTodos()
        {
            return _context.EstadosPostulacion
                .Include(e => e.Postulacion)
                .ToList();
        }

        // Obtener por Id
        public AttributesStatusApplication ObtenerPorId(int id)
        {
            return _context.EstadosPostulacion
                .Include(e => e.Postulacion)
                .FirstOrDefault(e => e.IdEstadoPostulacion == id);
        }

        // Agregar
        public void Agregar(AttributesStatusApplication estado)
        {
            _context.EstadosPostulacion.Add(estado);
            _context.SaveChanges();
        }

        // Eliminar
        public void Eliminar(int id)
        {
            var estado = _context.EstadosPostulacion.Find(id);
            if (estado != null)
            {
                _context.EstadosPostulacion.Remove(estado);
                _context.SaveChanges();
            }
        }

        // Actualizar
        public void Actualizar(AttributesStatusApplication estado)
        {
            var existente = _context.EstadosPostulacion.Find(estado.IdEstadoPostulacion);
            if (existente != null)
            {
                existente.Estado = estado.Estado;
                existente.FechaPostulacion = estado.FechaPostulacion;
                existente.IdPostulacion = estado.IdPostulacion;

                _context.SaveChanges();
            }
        }
    }
}
