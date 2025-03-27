using Incidencias.Core.Models;
using System.Collections.Generic;

namespace Incidencias.Services.Interfaces
{
    public interface ITecnicoService
    {
        IEnumerable<Tecnico> ObtenerTodosTecnicos();
        void CrearTecnico(Tecnico tecnico);
    }
}