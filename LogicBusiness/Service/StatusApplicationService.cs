using Common.Attributes;
using DataAccess.Repository;
using System.Collections.Generic;

namespace LogicBusiness.Service
{
    public class StatusApplicationService
    {
        private readonly StatusApplicationRepository _repository;

        public StatusApplicationService()
        {
            _repository = new StatusApplicationRepository();
        }

        // Listar todos los estados
        public List<AttributesStatusApplication> ListarTodos()
        {
            return _repository.ListarTodos();
        }

        // Obtener estado por Id
        public AttributesStatusApplication ObtenerPorId(int id)
        {
            return _repository.ObtenerPorId(id);
        }

        // Agregar nuevo estado
        public void Agregar(AttributesStatusApplication estado)
        {
            _repository.Agregar(estado);
        }

        // Eliminar estado por Id
        public void Eliminar(int id)
        {
            _repository.Eliminar(id);
        }

        // Actualizar estado
        public void Actualizar(AttributesStatusApplication estado)
        {
            _repository.Actualizar(estado);
        }
    }
}