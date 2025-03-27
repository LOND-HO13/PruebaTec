using Incidencias.Core.Models;
using Incidencias.Services.Interfaces;
using System.Web.Mvc;

namespace Incidencias.Web.Controllers
{
    public class TecnicoController : Controller
    {
        private readonly ITecnicoService _tecnicoService;

        public TecnicoController(ITecnicoService tecnicoService)
        {
            _tecnicoService = tecnicoService;
        }

        // GET: Tecnico/Create
        public ActionResult Create()
        {
            ViewBag.Tecnicos = _tecnicoService.ObtenerTodosTecnicos();
            return View();
        }

        // POST: Tecnico/Create (AJAX)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tecnico tecnico)
        {
            if (ModelState.IsValid)
            {
                _tecnicoService.CrearTecnico(tecnico);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_TecnicoRow", tecnico);
                }
                return RedirectToAction("Index");
            }
            return View(tecnico);
        }

        // GET: Tecnico/Index
        public ActionResult Index()
        {
            var tecnicos = _tecnicoService.ObtenerTodosTecnicos();
            return View(tecnicos);
        }
    }
}