using Common.Attributes;
using DataAccess.ConnectionDB;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repository
{
    public class WorkModalitiesRepository
    {
        private readonly RSContext _context;

        public WorkModalitiesRepository()
        {
            _context = new RSContext();
        }

        // Listar todas las modalidades
        public List<AttributesWorkModalities> ListarTodas()
        {
            return _context.ModalidadesTrabajo.ToList();
        }

        // Obtener modalidad por Id
        public AttributesWorkModalities ObtenerPorId(int id)
        {
            return _context.ModalidadesTrabajo.FirstOrDefault(m => m.IdModalidadTrabajo == id);
        }

        // Agregar nueva modalidad
        public void Agregar(AttributesWorkModalities modalidad)
        {
            _context.ModalidadesTrabajo.Add(modalidad);
            _context.SaveChanges();
        }

        // Eliminar modalidad
        public void Eliminar(int id)
        {
            var modalidad = _context.ModalidadesTrabajo.Find(id);
            if (modalidad != null)
            {
                _context.ModalidadesTrabajo.Remove(modalidad);
                _context.SaveChanges();
            }
        }

        // Actualizar modalidad
        public void Actualizar(AttributesWorkModalities modalidad)
        {
            var existente = _context.ModalidadesTrabajo.Find(modalidad.IdModalidadTrabajo);
            if (existente != null)
            {
                existente.Modalidad = modalidad.Modalidad;
                _context.SaveChanges();
            }
        }
    }
}
