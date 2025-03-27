using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Incidencias.Core.Models
{
    public class Incidencia
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio")]
        [MaxLength(100)]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        public string Descripcion { get; set; }

        [Required]
        public EstadoIncidencia Estado { get; set; } = EstadoIncidencia.Abierta;

        [Required]
        public PrioridadIncidencia Prioridad { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaActualizacion { get; set; } = DateTime.Now;

        // Relaciones
        public int UsuarioReportaId { get; set; }
        public virtual Usuario UsuarioReporta { get; set; }

        public int TecnicoAsignadoId { get; set; }
        public virtual Tecnico TecnicoAsignado { get; set; }

        [Required(ErrorMessage = "Debe agregar al menos un comentario")]
        public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
    }

    public enum EstadoIncidencia
    {
        Abierta,
        EnProgreso,
        Resuelta,
        Cerrada
    }

    public enum PrioridadIncidencia
    {
        Baja,
        Media,
        Alta,
        Critica
    }
}