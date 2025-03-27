using Incidencias.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidencias.Services.Interfaces
{
    public interface IIncidenciaService
    {
        void CrearIncidencia(Incidencia incidencia);
        IEnumerable<Incidencia> ObtenerTodas();

        IEnumerable<Incidencia> ObtenerIncidenciasFiltradas(string filtro = null);

        Incidencia ObtenerDetalleCompleto(int id);

        Incidencia ObtenerIncidenciaPorId(int id);

        void ActualizarIncidencia(Incidencia incidencia, string nuevoComentario, string autor);
    }
}
