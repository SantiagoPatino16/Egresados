using Common.Attributes;
using DataAccess.Repository;
using System.Collections.Generic;

namespace LogicBusiness.Service
{
    public class ApplicationsService
    {
        private readonly ApplicationsRepository _repository;

        public ApplicationsService()
        {
            _repository = new ApplicationsRepository();
        }

        public List<AttributesApplications> ListarTodas()
        {
            return _repository.ListarTodas();
        }

        public AttributesApplications ObtenerPorId(int id)
        {
            return _repository.ObtenerPorId(id);
        }

        public void Agregar(AttributesApplications postulacion)
        {
            _repository.Agregar(postulacion);
        }

        public void Eliminar(int id)
        {
            _repository.Eliminar(id);
        }

        public void Actualizar(AttributesApplications postulacion)
        {
            _repository.Actualizar(postulacion);
        }
    }
}