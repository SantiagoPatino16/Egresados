using Common.Attributes;
using DataAccess.Repository;
using System.Collections.Generic;

namespace LogicBusiness.Service
{
    public class WorkModalitiesService
    {
        private readonly WorkModalitiesRepository _repository;

        public WorkModalitiesService()
        {
            _repository = new WorkModalitiesRepository();
        }

        public List<AttributesWorkModalities> ListarTodas()
        {
            return _repository.ListarTodas();
        }

        public AttributesWorkModalities ObtenerPorId(int id)
        {
            return _repository.ObtenerPorId(id);
        }

        public void Agregar(AttributesWorkModalities modalidad)
        {
            _repository.Agregar(modalidad);
        }

        public void Eliminar(int id)
        {
            _repository.Eliminar(id);
        }

        public void Actualizar(AttributesWorkModalities modalidad)
        {
            _repository.Actualizar(modalidad);
        }
    }
}
