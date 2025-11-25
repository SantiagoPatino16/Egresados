using Common.Attributes;
using DataAccess.Repository;
using System.Collections.Generic;

namespace LogicBusiness.Service
{
    public class JobOffertsService
    {
        private readonly JobOffertsRepository _repository;

        public JobOffertsService()
        {
            _repository = new JobOffertsRepository();
        }

        public List<AttributesJobOfferts> ListarTodas()
        {
            return _repository.ListarTodas();
        }

        public AttributesJobOfferts ObtenerPorId(int id)
        {
            return _repository.ObtenerPorId(id);
        }

        public void Agregar(AttributesJobOfferts oferta)
        {
            _repository.Agregar(oferta);
        }

        public void Eliminar(int id)
        {
            _repository.Eliminar(id);
        }

        public void Actualizar(AttributesJobOfferts oferta)
        {
            _repository.Actualizar(oferta);
        }

        public List<AttributesCategoriesJobs> ListarCategorias()
        {
            return _repository.ListarCategorias();
        }

        public List<AttributesWorkModalities> ListarModalidades()
        {
            return _repository.ListarModalidades();
        }

        public List<AttributesContractType> ListarTiposContrato()
        {
            return _repository.ListarTiposContrato();
        }

    }
}
