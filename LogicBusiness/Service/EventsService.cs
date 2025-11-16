using Common.Attributes;
using DataAccess.Repository;
using System;
using System.Collections.Generic;

namespace LogicBusiness.Service
{
    public class EventsService
    {
        private readonly EventsRepository _repository;

        public EventsService()
        {
            _repository = new EventsRepository();
        }

        // Listar eventos activos
        public List<AttributesEvents> ListarEventos()
        {
            return _repository.Listar();
        }

        // Obtener evento por ID
        public AttributesEvents ObtenerEvento(int id)
        {
            return _repository.ObtenerPorId(id);
        }

        // Insertar nuevo evento
        public string CrearEvento(AttributesEvents evento)
        {
            if (string.IsNullOrEmpty(evento.Titulo))
                return "El título del evento es obligatorio.";

            if (evento.FechaInicio > evento.FechaFin)
                return "La fecha de inicio no puede ser mayor que la fecha de fin.";

            evento.Activo = true;
            evento.FechaCreacion = DateTime.Now;

            _repository.Insertar(evento);
            return "Evento registrado correctamente.";
        }

        // Editar evento
        public string ActualizarEvento(AttributesEvents evento)
        {
            if (evento == null)
                return "El evento no existe.";

            _repository.Actualizar(evento);
            return "Evento actualizado correctamente.";
        }

        // Eliminar (lógico)
        public string EliminarEvento(int id)
        {
            _repository.Eliminar(id);
            return "Evento eliminado correctamente.";
        }

        //Obtener imagen de evento
        public string ObtenerRutaImagenEvento(int id)
        {
            return _repository.ObtenerRutaImagen(id);
        }
        // Filtrar eventos por título,  y rango de fechas
        public List<AttributesEvents> FiltrarEventos(string titulo, string fechaInicio, string fechaFin)
        {
            DateTime? desde = string.IsNullOrWhiteSpace(fechaInicio) ? (DateTime?)null : DateTime.Parse(fechaInicio);
            DateTime? hasta = string.IsNullOrWhiteSpace(fechaFin) ? (DateTime?)null : DateTime.Parse(fechaFin);

            var eventos = _repository.Listar(); // todos los eventos activos

            if (!string.IsNullOrWhiteSpace(titulo))
                eventos = eventos.FindAll(e => e.Titulo.IndexOf(titulo, StringComparison.OrdinalIgnoreCase) >= 0);

            if (desde.HasValue)
                eventos = eventos.FindAll(e => e.FechaInicio >= desde.Value);

            if (hasta.HasValue)
                eventos = eventos.FindAll(e => e.FechaFin.HasValue && e.FechaFin.Value <= hasta.Value);
            return eventos;
        }

        // Filtrar por rango de fechas (solo fechas)
        public List<AttributesEvents> FiltrarPorFecha(DateTime desde, DateTime hasta)
        {
            return _repository.FiltrarPorFecha(desde, hasta);
        }

        // Obtener próximos eventos
        public List<AttributesEvents> ObtenerProximos()
        {
            return _repository.ObtenerProximos();
        }

    }
}
