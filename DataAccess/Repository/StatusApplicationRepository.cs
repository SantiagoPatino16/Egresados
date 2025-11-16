using Common.Attributes;
using DataAccess.ConnectionDB;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repository
{
    public class StatusApplicationRepository
    {
        private readonly RSContext _context;

        public StatusApplicationRepository()
        {
            _context = new RSContext();
        }

        // ➤ Listar todos los estados con su Postulación
        public List<AttributesStatusApplication> ListarTodos()
        {
            return _context.EstadoPostulacion
                .Include(e => e.Postulacion)
                .ToList();
        }

        // ➤ Obtener un estado por ID
        public AttributesStatusApplication ObtenerPorId(int id)
        {
            return _context.EstadoPostulacion
                .Include(e => e.Postulacion)
                .FirstOrDefault(e => e.IdEstadoPostulacion == id);
        }

        // ➤ Agregar un nuevo estado
        public void Agregar(AttributesStatusApplication estado)
        {
            _context.EstadoPostulacion.Add(estado);
            _context.SaveChanges();
        }

        // ➤ Eliminar estado por ID
        public void Eliminar(int id)
        {
            var estado = _context.EstadoPostulacion.Find(id);

            if (estado != null)
            {
                _context.EstadoPostulacion.Remove(estado);
                _context.SaveChanges();
            }
        }

        // ➤ Actualizar un estado existente
        public void Actualizar(AttributesStatusApplication estado)
        {
            var existente = _context.EstadoPostulacion.Find(estado.IdEstadoPostulacion);

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
