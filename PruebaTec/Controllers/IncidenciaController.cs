using Incidencias.Services.Interfaces;
using Incidencias.Core.Models;
using System.Web.Mvc;
using System;
using System.Linq;
using PagedList;

namespace Incidencias.Web.Controllers
{
    public class IncidenciaController : Controller
    {
        private readonly IIncidenciaService _incidenciaService;
        private readonly ITecnicoService _tecnicoService;
        private readonly IUsuarioService _usuarioService;
        private readonly IReporteService _reporteService;

        public IncidenciaController(
            IIncidenciaService incidenciaService,
            ITecnicoService tecnicoService,
            IUsuarioService usuarioService,
            IReporteService reporteService)
        {
            _incidenciaService = incidenciaService;
            _reporteService = reporteService;
            _tecnicoService = tecnicoService;
            _usuarioService = usuarioService;
        }

         public ActionResult ReporteEstadisticas()
    {
        var reporte = _reporteService.GenerarReporteIncidencias();
        return View(reporte);
    }

        // GET: Incidencia/Index
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            // Configurar parámetros de paginación y ordenación
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParam = sortOrder == "Fecha" ? "fecha_desc" : "Fecha";

            // Manejar búsqueda/filtros
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            // Obtener incidencias paginadas desde el servicio
            var incidencias = _incidenciaService.ObtenerIncidenciasFiltradas(searchString);

            // Ordenar
            switch (sortOrder)
            {
                case "name_desc":
                    incidencias = incidencias.OrderByDescending(i => i.Titulo);
                    break;
                case "Fecha":
                    incidencias = incidencias.OrderBy(i => i.FechaCreacion);
                    break;
                case "fecha_desc":
                    incidencias = incidencias.OrderByDescending(i => i.FechaCreacion);
                    break;
                default:
                    incidencias = incidencias.OrderBy(i => i.Titulo);
                    break;
            }

            // Configurar paginación
            int pageSize = 10; // Elementos por página
            int pageNumber = (page ?? 1);
            return View(incidencias.ToPagedList(pageNumber, pageSize));
        }

        // GET: Incidencia/Create
        public ActionResult Create()
        {
            // Obtener listas desde los servicios
            var tecnicos = _tecnicoService.ObtenerTodosTecnicos();
            var usuarios = _usuarioService.ObtenerTodosUsuarios();

            // Pasar a la vista
            ViewBag.TecnicoAsignadoId = new SelectList(tecnicos, "Id", "Nombre");
            ViewBag.UsuarioReportaId = new SelectList(usuarios, "Id", "Nombre");

            return View();
        }

        // POST: Incidencia/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Incidencia incidencia, string comentarioInicial)
        {
            if (ModelState.IsValid)
            {
                // Validar comentario inicial
                if (string.IsNullOrWhiteSpace(comentarioInicial))
                {
                    ModelState.AddModelError("", "Debe agregar un comentario inicial");
                    RecargarDropdowns();
                    return View(incidencia);
                }

                // Asignar autor al comentario
                incidencia.Comentarios.Add(new Comentario
                {
                    Contenido = comentarioInicial,
                    Fecha = DateTime.Now,
                    Autor = User.Identity.Name ?? "Sistema" // <--- ¡Aquí se asigna el autor!
                });

                _incidenciaService.CrearIncidencia(incidencia);
                return RedirectToAction("Index");
            }

            RecargarDropdowns();
            return View(incidencia);
        }

        private void RecargarDropdowns()
        {
            ViewBag.TecnicoAsignadoId = new SelectList(_tecnicoService.ObtenerTodosTecnicos(), "Id", "Nombre");
            ViewBag.UsuarioReportaId = new SelectList(_usuarioService.ObtenerTodosUsuarios(), "Id", "Nombre");
        }

        // En IncidenciaController.cs
        public ActionResult Edit(int id)
        {
            var incidencia = _incidenciaService.ObtenerIncidenciaPorId(id);

            if (incidencia == null)
            {
                return HttpNotFound();
            }

            // Cargar listas para dropdowns
            ViewBag.TecnicoAsignadoId = new SelectList(
                _tecnicoService.ObtenerTodosTecnicos(),
                "Id",
                "Nombre",
                incidencia.TecnicoAsignadoId
            );

            ViewBag.UsuarioReportaId = new SelectList(
                _usuarioService.ObtenerTodosUsuarios(),
                "Id",
                "Nombre",
                incidencia.UsuarioReportaId
            );

            ViewBag.Comentarios = incidencia.Comentarios.ToList();


            return View(incidencia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Incidencia incidencia, string nuevoComentario)
        {
            if (ModelState.IsValid)
            {
                string autor = User.Identity.IsAuthenticated
                    ? User.Identity.Name
                    : "Sistema";

                _incidenciaService.ActualizarIncidencia(
                    incidencia,
                    nuevoComentario?.Trim(),
                    autor // <- Pasar el autor aquí
                );

                return RedirectToAction("Index");
            }

            RecargarDropdowns();
            return View(incidencia);
        }

        public ActionResult Details(int id)
        {
            var incidencia = _incidenciaService.ObtenerDetalleCompleto(id);

            if (incidencia == null)
            {
                return HttpNotFound();
            }

            // Preparar datos adicionales para la vista
            ViewBag.EstadosDisponibles = Enum.GetValues(typeof(EstadoIncidencia));
            ViewBag.PrioridadesDisponibles = Enum.GetValues(typeof(PrioridadIncidencia));

            return View(incidencia);
        }
    }


}