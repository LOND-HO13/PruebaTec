using Incidencias.Core.Models;
using System.Collections.Generic;

namespace Incidencias.Services.Interfaces
{
    public interface IUsuarioService
    {
        IEnumerable<Usuario> ObtenerTodosUsuarios();
        void CrearUsuario(Usuario usuario);
    }
}