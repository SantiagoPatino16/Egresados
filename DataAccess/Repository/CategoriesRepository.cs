using Common.Attributes;
using DataAccess.ConnectionDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CategoriesRepository
    {
        private readonly RSContext _context;
        // Aquí irían los métodos para manejar las categorías
        public CategoriesRepository()
        {
            _context = new RSContext();
        }

        // Listar todas las categorías
        public List<AttributesCategories> ListarTodas()
        {
            return _context.Categorias.ToList();
        }

        // Obtener categoría por Id
        public AttributesCategories ObtenerPorId(int id)
        {
            return _context.Categorias.FirstOrDefault(c => c.IdCategoria == id);
        }

        // Agregar nueva categoría

        public void Agregar(AttributesCategories categoria)
        {
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
        }

        // Eliminar categoría

        public void Eliminar(int id)
        {
            var cat = _context.Categorias.Find(id);
            if (cat != null)
            {
                _context.Categorias.Remove(cat);
                _context.SaveChanges();
            }
        }
    }
}
