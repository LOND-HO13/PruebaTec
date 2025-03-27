using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidencias.Core.Models
{
    public class ReporteEstadisticasVM
    {
        public Dictionary<EstadoIncidencia, int> IncidenciasPorEstado { get; set; }
        public Dictionary<PrioridadIncidencia, int> IncidenciasPorPrioridad { get; set; }
        public int TotalIncidencias { get; set; }
    }
}
