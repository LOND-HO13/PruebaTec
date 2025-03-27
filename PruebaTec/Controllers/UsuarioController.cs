using Incidencias.Core.Models;
using Incidencias.Services.Interfaces;
using System.Web.Mvc;

namespace Incidencias.Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            ViewBag.Usuarios = _usuarioService.ObtenerTodosUsuarios();
            return View();
        }

        // POST: Usuario/Create (AJAX)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _usuarioService.CrearUsuario(usuario);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_UsuarioRow", usuario);
                }
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // GET: Usuario/Index
        public ActionResult Index()
        {
            var usuarios = _usuarioService.ObtenerTodosUsuarios();
            return View(usuarios);
        }
    }
}