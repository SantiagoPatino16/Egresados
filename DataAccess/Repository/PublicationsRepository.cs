using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Attributes;
using Common.Entities;
using DataAccess.ConnectionDB;

namespace DataAccess.Repository
{
    public class PublicationsRepository
    {
        private readonly RSContext _context;

        public PublicationsRepository()
        {
            _context = new RSContext();
        }



        // Listar todas las publicaciones con sus comentarios y usuario
        public List<AttributesPublications> Listar()
        {
            return _context.Publicaciones
                           .Include("Usuario")
                           .Include("Comentarios.Usuario")
                           .OrderByDescending(p => p.Fecha)
                           .ToList();
        }

        // Agregar nueva publicación
        public void Agregar(AttributesPublications publicacion)
        {
            _context.Publicaciones.Add(publicacion);
            _context.SaveChanges();
        }

        // Obtener publicación por id
        public AttributesPublications ObtenerPorId(int id)
        {
            return _context.Publicaciones
                           .Include("Usuario")
                           .Include("Comentarios.Usuario")
                           .FirstOrDefault(p => p.IdPublicacion == id);
        }

        // Eliminar publicación
        public void Eliminar(int id)
        {
            var pub = _context.Publicaciones.Find(id);
            if (pub != null)
            {
                _context.Publicaciones.Remove(pub);
                _context.SaveChanges();
            }
        }
    }
}
