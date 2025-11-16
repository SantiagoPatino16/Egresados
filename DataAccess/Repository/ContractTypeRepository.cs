using Common.Attributes;
using DataAccess.ConnectionDB;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repository
{
    public class ContractTypeRepository
    {
        private readonly RSContext _context;

        public ContractTypeRepository()
        {
            _context = new RSContext();
        }

        // Listar todos los tipos de contrato
        public List<AttributesContractType> ListarTodos()
        {
            return _context.TiposContrato.ToList();
        }

        // Obtener tipo de contrato por Id
        public AttributesContractType ObtenerPorId(int id)
        {
            return _context.TiposContrato.FirstOrDefault(c => c.IdTipoContrato == id);
        }

        // Agregar un nuevo tipo de contrato
        public void Agregar(AttributesContractType tipoContrato)
        {
            _context.TiposContrato.Add(tipoContrato);
            _context.SaveChanges();
        }

        // Eliminar tipo de contrato
        public void Eliminar(int id)
        {
            var contrato = _context.TiposContrato.Find(id);
            if (contrato != null)
            {
                _context.TiposContrato.Remove(contrato);
                _context.SaveChanges();
            }
        }

        // Actualizar tipo de contrato
        public void Actualizar(AttributesContractType tipoContrato)
        {
            var existente = _context.TiposContrato.Find(tipoContrato.IdTipoContrato);
            if (existente != null)
            {
                existente.TipoContrato = tipoContrato.TipoContrato;
                _context.SaveChanges();
            }
        }
    }
}