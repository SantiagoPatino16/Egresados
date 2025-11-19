using Common.Attributes;
using DataAccess.ConnectionDB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repository
{
    public class EventsRepository
    {
        private readonly RSContext _context;

        public EventsRepository()
        {
            _context = new RSContext();
        }

        // Listar todos los eventos
        public List<AttributesEvents> Listar()
        {
            return _context.Eventos
                .Where(e => e.Activo)
                .OrderByDescending(e => e.FechaInicio)
                .ToList();
        }

        // Buscar por ID
        public AttributesEvents ObtenerPorId(int id)
        {
            return _context.Eventos.FirstOrDefault(e => e.IdEvento == id);
        }

        // Agregar nuevo evento
        public void Insertar(AttributesEvents evento)
        {
            _context.Eventos.Add(evento);
            _context.SaveChanges();
        }

        // Editar evento existente
        public void Actualizar(AttributesEvents evento)
        {
            _context.Entry(evento).State = EntityState.Modified;
            _context.SaveChanges();
        }

        // Eliminar (lógico)
        public void Eliminar(int id)
        {
            var evento = _context.Eventos.FirstOrDefault(e => e.IdEvento == id);
            if (evento != null)
            {
                evento.Activo = false;
                _context.SaveChanges();
            }
        }

        // Buscar por título o palabra clave
        public List<AttributesEvents> Buscar(string termino)
        {
            return _context.Eventos
                .Where(e => e.Activo &&
                            (e.Titulo.Contains(termino) || e.Descripcion.Contains(termino)))
                .OrderByDescending(e => e.FechaInicio)
                .ToList();
        }

        // Filtrar por rango de fechas
        public List<AttributesEvents> FiltrarPorFecha(DateTime desde, DateTime hasta)
        {
            return _context.Eventos
                .Where(e => e.Activo &&
                            e.FechaInicio >= desde &&
                            e.FechaFin <= hasta)
                .OrderBy(e => e.FechaInicio)
                .ToList();
        }

        // Obtener próximos eventos (ordenados por cercanía)
        public List<AttributesEvents> ObtenerProximos()
        {
            var ahora = DateTime.Now;
            
            return _context.Eventos
                .Where(e => e.Activo && 
                           (e.FechaInicio >= ahora || // Eventos que aún no han comenzado
                           (e.FechaFin.HasValue && e.FechaFin.Value >= ahora))) // O eventos en curso
                .OrderBy(e => e.FechaInicio) // Primero los más próximos
                .ThenByDescending(e => e.FechaCreacion) // Luego los más recientes
                .ToList();
        }

        // Obtener la ruta de imagen de un evento
        public string ObtenerRutaImagen(int idEvento)
        {
            var evento = _context.Eventos
                .Where(e => e.IdEvento == idEvento && e.Activo)
                .Select(e => e.RutaImagen)
                .FirstOrDefault();

            return evento ?? string.Empty;
        }
    }
}
