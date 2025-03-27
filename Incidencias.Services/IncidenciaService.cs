using Incidencias.Core.Models;
using Incidencias.Data;
using Incidencias.Data.Interfaces;
using Incidencias.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Data.Entity;


namespace Incidencias.Services
{
    public class IncidenciaService : IIncidenciaService
    {
        private readonly IRepository<Incidencia> _repository;
        private readonly IncidenciasDbContext _context;

        public IncidenciaService(IRepository<Incidencia> repository, IncidenciasDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        public void CrearIncidencia(Incidencia incidencia)
        {
            // Validación básica
            if (string.IsNullOrEmpty(incidencia.Titulo))
                throw new System.ArgumentException("El título es obligatorio");

            if (!incidencia.Comentarios.Any())
            {
                throw new InvalidOperationException("Toda incidencia debe tener al menos un comentario");
            }

            foreach (var comentario in incidencia.Comentarios)
            {
                if (string.IsNullOrEmpty(comentario.Autor))
                {
                    comentario.Autor = "Sistema"; // Valor por defecto
                }
            }

            _repository.Add(incidencia);
            _repository.Save();
        }

        public IEnumerable<Incidencia> ObtenerTodas() => _repository.GetAll();

        public IEnumerable<Incidencia> ObtenerIncidenciasFiltradas(string filtro = null)
        {
            var query = _repository.GetAll();

            if (!string.IsNullOrEmpty(filtro))
            {
                query = query.Where(i =>
                    i.Titulo.Contains(filtro) ||
                    i.Descripcion.Contains(filtro)
                );
            }

            return query;
        }

        public Incidencia ObtenerDetalleCompleto(int id)
        {
            return _context.Incidencias
            .Include(i => i.UsuarioReporta)
            .Include(i => i.TecnicoAsignado)
            .Include(i => i.Comentarios)
            .AsNoTracking()
            .FirstOrDefault(i => i.Id == id);
        }

        public Incidencia ObtenerIncidenciaPorId(int id)
        {
            return _repository.GetById(id);
        }

        public void ActualizarIncidencia(Incidencia incidencia, string nuevoComentario, string autor)
        {
            var incidenciaExistente = _context.Incidencias
                .Include(i => i.Comentarios)
                .FirstOrDefault(i => i.Id == incidencia.Id);

            if (incidenciaExistente != null)
            {
                // Actualizar propiedades
                _context.Entry(incidenciaExistente).CurrentValues.SetValues(incidencia);

                // Agregar comentario si existe
                if (!string.IsNullOrWhiteSpace(nuevoComentario))
                {
                    incidenciaExistente.Comentarios.Add(new Comentario
                    {
                        Contenido = nuevoComentario.Trim(),
                        Autor = autor ?? "Sistema", // Usar el parámetro
                        Fecha = DateTime.Now
                    });
                }

                _context.SaveChanges();
            }
        }
    }
}