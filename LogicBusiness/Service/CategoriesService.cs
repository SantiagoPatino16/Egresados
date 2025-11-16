using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicBusiness.Service
{
    public class CategoriesService
    {
        private readonly CategoriesRepository _repo;
        public CategoriesService()
        {
            _repo = new CategoriesRepository();
        }

        //listar todas las categorías
        public List<Common.Attributes.AttributesCategories> ListarTodas()
        {
            return _repo.ListarTodas();
        }

        // Obtener categoría por Id
        public Common.Attributes.AttributesCategories Obtener(int id)
        {
            return _repo.ObtenerPorId(id);
        }

        // Agregar nueva categoría
        public void Crear(Common.Attributes.AttributesCategories categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria.Nombre))
            {
                throw new ArgumentException("El nombre de la categoría es obligatorio.");
            }
            _repo.Agregar(categoria);
        }

        // Eliminar categoría
        public void Eliminar(int id)
        {
            _repo.Eliminar(id);
        }

    }
}
