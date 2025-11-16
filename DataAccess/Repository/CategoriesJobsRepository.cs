using Common.Attributes;
using DataAccess.ConnectionDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CategoriesJobsRepository
    {
        private readonly RSContext _context;
        public CategoriesJobsRepository()
        {
            _context = new RSContext();
        }
        // Listar todas las categorías de trabajos
        public List<AttributesCategoriesJobs> ListarTodas()
        {
            return _context.CategoriasEmpleo.ToList();
        }

        // Obtener categoría de trabajo por Id
        public AttributesCategoriesJobs ObtenerPorId(int id)
        {
            return _context.CategoriasEmpleo.FirstOrDefault(c => c.IdCategoria == id);
        }
        // Agregar nueva categoría de trabajo
        public void Agregar(AttributesCategoriesJobs categoria)
        {
            _context.CategoriasEmpleo.Add(categoria);
            _context.SaveChanges();
        }
        // Eliminar categoría de trabajo
        public void Eliminar(int id)
        {
            var cat = _context.CategoriasEmpleo.Find(id);
            if (cat != null)
            {
                _context.CategoriasEmpleo.Remove(cat);
                _context.SaveChanges();
            }
        }
        //Update categoría de trabajo
        public void Actualizar(AttributesCategoriesJobs categoria)
        {
            var catExistente = _context.CategoriasEmpleo.Find(categoria.IdCategoria);
            if (catExistente != null)
            {
                catExistente.Categoria = categoria.Categoria;
                _context.SaveChanges();
            }
        }
    }
}
