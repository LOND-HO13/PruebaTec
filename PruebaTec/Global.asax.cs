using Incidencias.Core.Models;
using Incidencias.Data.Interfaces;
using Incidencias.Data.Repositories;
using Incidencias.Services;
using Incidencias.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;
using Unity.AspNet.Mvc;

namespace PruebaTec
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = new UnityContainer();
            container.RegisterType<IRepository<Incidencia>, Repository<Incidencia>>();
            container.RegisterType<IIncidenciaService, IncidenciaService>();
            container.RegisterType<ITecnicoService, TecnicoService>();
            container.RegisterType<IUsuarioService, UsuarioService>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
