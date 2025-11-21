using Common.Attributes;
using DataAccess.ConnectionDB;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repository
{
    public class JobOffertsRepository
    {
        private readonly RSContext _context;

        public JobOffertsRepository()
        {
            _context = new RSContext();
        }

        // Listar todas las ofertas
        public List<AttributesJobOfferts> ListarTodas()
        {
            return _context.OfertasEmpleo.ToList();
        }

        // Obtener oferta por Id
        public AttributesJobOfferts ObtenerPorId(int id)
        {
            return _context.OfertasEmpleo.FirstOrDefault(o => o.IdOferta == id);
        }

        // Agregar oferta
        public void Agregar(AttributesJobOfferts oferta)
        {
            _context.OfertasEmpleo.Add(oferta);
            _context.SaveChanges();
        }

        // Eliminar oferta
        public void Eliminar(int id)
        {
            var oferta = _context.OfertasEmpleo.Find(id);
            if (oferta != null)
            {
                _context.OfertasEmpleo.Remove(oferta);
                _context.SaveChanges();
            }
        }

        // Actualizar oferta
        public void Actualizar(AttributesJobOfferts oferta)
        {
            var existente = _context.OfertasEmpleo.Find(oferta.IdOferta);
            if (existente != null)
            {
                existente.Titulo = oferta.Titulo;
                existente.Descripcion = oferta.Descripcion;
                existente.Requisitos = oferta.Requisitos;
                existente.Salario = oferta.Salario;
                existente.Ciudad = oferta.Ciudad;
                existente.FechaPublicacion = oferta.FechaPublicacion;
                existente.FechaCierre = oferta.FechaCierre;
                existente.Estado = oferta.Estado;

                existente.IdEmpresa = oferta.IdEmpresa;
                existente.IdTipoContrato = oferta.IdTipoContrato;
                existente.IdModalidadTrabajo = oferta.IdModalidadTrabajo;
                existente.IdCategoria = oferta.IdCategoria;

                _context.SaveChanges();
            }
        }

        public List<AttributesCategoriesJobs> ListarCategorias()
        {
            return _context.CategoriasEmpleo.ToList();
        }

        public List<AttributesWorkModalities> ListarModalidades()
        {
            return _context.ModalidadesTrabajo.ToList();
        }

        public List<AttributesContractType> ListarTiposContrato()
        {
            return _context.TiposContrato.ToList();
        }

    }
}
