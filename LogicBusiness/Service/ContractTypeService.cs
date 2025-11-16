using Common.Attributes;
using DataAccess.Repository;
using System.Collections.Generic;

namespace LogicBusiness.Service
{
    public class ContractTypeService
    {
        private readonly ContractTypeRepository _repository;

        public ContractTypeService()
        {
            _repository = new ContractTypeRepository();
        }

        // Listar todos los tipos de contrato
        public List<AttributesContractType> ListarTodos()
        {
            return _repository.ListarTodos();
        }

        // Obtener tipo de contrato por Id
        public AttributesContractType ObtenerPorId(int id)
        {
            return _repository.ObtenerPorId(id);
        }

        // Agregar un nuevo tipo de contrato
        public void Agregar(AttributesContractType tipo)
        {
            _repository.Agregar(tipo);
        }

        // Eliminar tipo de contrato
        public void Eliminar(int id)
        {
            _repository.Eliminar(id);
        }

        // Actualizar tipo de contrato
        public void Actualizar(AttributesContractType tipo)
        {
            _repository.Actualizar(tipo);
        }
    }
}
