using Incidencias.Core.Models;
using Incidencias.Data;
using Incidencias.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidencias.Services
{
    public class ReporteService : IReporteService
    {
        private readonly IncidenciasDbContext _context;

        public ReporteService(IncidenciasDbContext context)
        {
            _context = context;
        }

        public ReporteEstadisticasVM GenerarReporteIncidencias()
        {
            var incidencias = _context.Incidencias.ToList();

            return new ReporteEstadisticasVM
            {
                IncidenciasPorEstado = incidencias
                    .GroupBy(i => i.Estado) 
                    .ToDictionary(g => g.Key, g => g.Count()),

                IncidenciasPorPrioridad = incidencias
                    .GroupBy(i => i.Prioridad) 
                    .ToDictionary(g => g.Key, g => g.Count()),

                TotalIncidencias = incidencias.Count
            };
        }

    }
}
