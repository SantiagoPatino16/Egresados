using Common.Attributes;
using DataAccess.Repository;
using System.Collections.Generic;

namespace LogicBusiness.Service
{
    public class FavoritesJobsService
    {
        private readonly FavoritesJobsRepository _repository;

        public FavoritesJobsService()
        {
            _repository = new FavoritesJobsRepository();
        }

        // Listar todos
        public List<AttributesFavoritesJobs> ListarTodos()
        {
            return _repository.ListarTodos();
        }

        // Obtener por Id
        public AttributesFavoritesJobs ObtenerPorId(int id)
        {
            return _repository.ObtenerPorId(id);
        }

        // Listar favoritos de un egresado
        public List<AttributesFavoritesJobs> ListarPorEgresado(int idEgresado)
        {
            return _repository.ListarPorEgresado(idEgresado);
        }

        // Agregar favorito
        public void Agregar(AttributesFavoritesJobs favorito)
        {
            _repository.Agregar(favorito);
        }

        // Eliminar favorito por IdFavorito
        public void Eliminar(int idFavorito)
        {
            _repository.Eliminar(idFavorito);
        }

        // Eliminar favorito por usuario y oferta
        public void EliminarPorUsuarioYOferta(int idEgresado, int idOferta)
        {
            _repository.EliminarPorUsuarioYOferta(idEgresado, idOferta);
        }
    }
}
