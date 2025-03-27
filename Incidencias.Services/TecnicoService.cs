using Incidencias.Core.Models;
using Incidencias.Data.Interfaces;
using Incidencias.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Incidencias.Services
{
    public class TecnicoService : ITecnicoService
    {
        private readonly IRepository<Tecnico> _repository;

        public TecnicoService(IRepository<Tecnico> repository)
        {
            _repository = repository;
        }

        public IEnumerable<Tecnico> ObtenerTodosTecnicos()
        {
            return _repository.GetAll();
        }

        public void CrearTecnico(Tecnico tecnico)
        {
            if (string.IsNullOrEmpty(tecnico.Nombre))
                throw new ArgumentException("El nombre es obligatorio");

            _repository.Add(tecnico);
            _repository.Save();
        }
    }
}