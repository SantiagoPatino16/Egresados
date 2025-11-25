using Common.Attributes;
using DataAccess.ConnectionDB;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public void CrearEstado(AttributesStatusApplication estado)
        {
            using (var context = new RSContext())
            {
                context.EstadoPostulacion.Add(estado);
                context.SaveChanges();
            }
        }

        // Listar postulaciones por empresa
        public List<AttributesApplications> ListarPorEmpresa(int idEmpresa)
        {
            return _repository.ListarPorEmpresa(idEmpresa);
        }

        // Método para verificar si un egresado ya está postulado a una oferta
        public bool EsPostulado(int idOferta, int idEgresado)
        {
            using (var context = new RSContext())
            {
                return context.Postulaciones
                    .Any(p => p.IdOferta == idOferta && p.IdEgresado == idEgresado && p.EstadoFinal == AttributesApplications.EstadoPostulacion.Postulado);
            }
        }

        // Método para realizar una postulación
        public void Postular(int idOferta, int idEgresado)
        {
            using (var context = new RSContext())
            {
                var postulacion = new AttributesApplications
                {
                    IdOferta = idOferta,
                    IdEgresado = idEgresado,
                    FechaPostulacion = DateTime.Now,
                    EstadoFinal = AttributesApplications.EstadoPostulacion.Postulado
                };
                context.Postulaciones.Add(postulacion);
                context.SaveChanges();
            }
        }
    }
}
