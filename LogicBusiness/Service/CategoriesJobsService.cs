using Common.Attributes;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicBusiness.Service
{
    public class CategoriesJobsService
    {
        private readonly CategoriesJobsRepository _repository;

        public CategoriesJobsService()
        {
            _repository = new CategoriesJobsRepository();
        }

        // Listar todas las categorías de trabajos
        public List<AttributesCategoriesJobs> ListarTodas()
        {
            return _repository.ListarTodas();
        }

        // Obtener categoría de trabajo por Id
        public AttributesCategoriesJobs ObtenerPorId(int id)
        {
            return _repository.ObtenerPorId(id);
        }

        // Agregar nueva categoría de trabajo
        public void Agregar(AttributesCategoriesJobs categoria)
        {
            _repository.Agregar(categoria);
        }

        // Eliminar categoría de trabajo
        public void Eliminar(int id)
        {
            _repository.Eliminar(id);            
        }

        // Actualizar categoría de trabajo
        public void Actualizar(AttributesCategoriesJobs categoria)
        {
            _repository.Actualizar(categoria);
        }
    }
}
