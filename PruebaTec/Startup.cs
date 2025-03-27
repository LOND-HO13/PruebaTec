using Microsoft.Owin;
using Owin;
using Unity;
using Incidencias.Data.Repositories;
using Incidencias.Services;
using Unity.AspNet.Mvc;
using System.Web.Mvc;
using Incidencias.Data.Interfaces;
using Incidencias.Core.Models;
using Incidencias.Data;
using Incidencias.Services.Interfaces;

[assembly: OwinStartup(typeof(Incidencias.Web.Startup))]
namespace Incidencias.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configurar el contenedor Unity para Dependency Injection
            var container = new UnityContainer();

            // Registrar el DbContext (usa la misma cadena de conexión que en Web.config)
            container.RegisterType<IncidenciasDbContext>();

            // Registrar Repositorios
            container.RegisterType<IRepository<Incidencia>, Repository<Incidencia>>();
            container.RegisterType<IIncidenciaService, IncidenciaService>();
            container.RegisterType<IRepository<Tecnico>, Repository<Tecnico>>(); // Añadir esta línea
            container.RegisterType<IRepository<Usuario>, Repository<Usuario>>(); // Añadir esta línea
            container.RegisterType<IReporteService, ReporteService>();


            // Registrar Servicios
            container.RegisterType<ITecnicoService, TecnicoService>();
            container.RegisterType<IUsuarioService, UsuarioService>();
            container.RegisterType<IIncidenciaService, IncidenciaService>();

            // Establecer el resolver de dependencias para MVC
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

        }
    }
}