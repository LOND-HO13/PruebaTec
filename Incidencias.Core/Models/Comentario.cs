using System;
using System.ComponentModel.DataAnnotations;

namespace Incidencias.Core.Models
{
    public class Comentario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El contenido del comentario es obligatorio")]
        public string Contenido { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required]
        public string Autor { get; set; }  // Nueva propiedad

        public bool EsInterno { get; set; } // Nueva propiedad

        // Relación con Incidencia
        public int IncidenciaId { get; set; }
        public virtual Incidencia Incidencia { get; set; }
    }
}