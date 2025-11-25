using Common.Attributes;
using DataAccess.ConnectionDB;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace DataAccess.Repository
{
    public class FavoritesJobsRepository
    {
        private readonly RSContext _context;

        public FavoritesJobsRepository()
        {
            _context = new RSContext();
        }

        // Listar todos los favoritos
        public List<AttributesFavoritesJobs> ListarTodos()
        {
            return _context.OfertasFavoritas.ToList();
        }

        // Obtener favorito por Id
        public AttributesFavoritesJobs ObtenerPorId(int idFavorito)
        {
            return _context.OfertasFavoritas.FirstOrDefault(f => f.IdFavorito == idFavorito);
        }

        // Listar favoritos por Egresado
        public List<AttributesFavoritesJobs> ListarPorEgresado(int idEgresado)
        {
            return _context.OfertasFavoritas
                .Include("Oferta")   // CARGA TODA LA OFERTA
                .Include("Egresado") // opcional
                .Where(f => f.IdEgresado == idEgresado)
                .ToList();
        }

        // Agregar un favorito
        public void Agregar(AttributesFavoritesJobs favorito)
        {
            _context.OfertasFavoritas.Add(favorito);
            _context.SaveChanges();
        }

        // Eliminar un favorito
        public void Eliminar(int idFavorito)
        {
            var fav = _context.OfertasFavoritas.Find(idFavorito);
            if (fav != null)
            {
                _context.OfertasFavoritas.Remove(fav);
                _context.SaveChanges();
            }
        }

        // Eliminar favorito por usuario y oferta
        public void EliminarPorUsuarioYOferta(int idEgresado, int idOferta)
        {
            var fav = _context.OfertasFavoritas
                .FirstOrDefault(f => f.IdEgresado == idEgresado && f.IdOferta == idOferta);

            if (fav != null)
            {
                _context.OfertasFavoritas.Remove(fav);
                _context.SaveChanges();
            }
        }
    }
}
