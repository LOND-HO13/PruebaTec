using Incidencias.Core.Models;
using Incidencias.Data.Interfaces;
using Incidencias.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Incidencias.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IRepository<Usuario> _repository;

        public UsuarioService(IRepository<Usuario> repository)
        {
            _repository = repository;
        }

        public IEnumerable<Usuario> ObtenerTodosUsuarios()
        {
            return _repository.GetAll();
        }

        public void CrearUsuario(Usuario usuario)
        {
            if (string.IsNullOrEmpty(usuario.Nombre))
                throw new ArgumentException("El nombre es obligatorio");

            _repository.Add(usuario);
            _repository.Save();
        }
    }
}