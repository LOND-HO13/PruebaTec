using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidencias.Core.Models
{
    public class Tecnico
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del técnico es obligatorio")]
        [MaxLength(50)]
        public string Nombre { get; set; }
    }
}
