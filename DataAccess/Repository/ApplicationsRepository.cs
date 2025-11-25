using Common.Attributes;
using DataAccess.ConnectionDB;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repository
{
    public class ApplicationsRepository
    {
        private readonly RSContext _context;

        public ApplicationsRepository()
        {
            _context = new RSContext();
        }

        public List<AttributesApplications> ListarTodas()
        {
            return _context.Postulaciones
                .Include(p => p.Egresado)
                .Include(p => p.OfertaEmpleo)
                .ToList();
        }

        public AttributesApplications ObtenerPorId(int id)
        {
            return _context.Postulaciones
                .Include(p => p.Egresado)
                .Include(p => p.OfertaEmpleo)
                .FirstOrDefault(p => p.IdPostulacion == id);
        }

        public void Agregar(AttributesApplications postulacion)
        {
            _context.Postulaciones.Add(postulacion);
            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var postulacion = _context.Postulaciones.Find(id);
            if (postulacion != null)
            {
                _context.Postulaciones.Remove(postulacion);
                _context.SaveChanges();
            }
        }

        public void Actualizar(AttributesApplications postulacion)
        {
            var existente = _context.Postulaciones.Find(postulacion.IdPostulacion);
            if (existente != null)
            {
                existente.IdOferta = postulacion.IdOferta;
                existente.IdEgresado = postulacion.IdEgresado;
                existente.FechaPostulacion = postulacion.FechaPostulacion;
                existente.EstadoFinal = postulacion.EstadoFinal;
                existente.Mensaje = postulacion.Mensaje;
                existente.CvUrl = postulacion.CvUrl;

                _context.SaveChanges();
            }
        }
        public List<AttributesApplications> ListarPorEmpresa(int idEmpresa)
        {
            return _context.Postulaciones
                .Include(p => p.Egresado)
                .Include(p => p.OfertaEmpleo)
                .Include(p => p.OfertaEmpleo.Empresa)
                .Where(p => p.OfertaEmpleo.IdEmpresa == idEmpresa)
                .ToList();
        }
    }
}