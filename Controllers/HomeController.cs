using System.Diagnostics;
using GestorTareas.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GestorTareas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var tareas = _context.Tareas.ToList();
            return View(tareas);
        }

        [HttpPost]
        public IActionResult AgregarTarea(string nuevaTarea)
        {
            if (!string.IsNullOrEmpty(nuevaTarea))
            {
                var tarea = new Tarea
                {
                    Nombre = nuevaTarea
                };

                _context.Tareas.Add(tarea);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EliminarTarea(int id)
        {
            var tarea = _context.Tareas.Find(id);

            if (tarea != null)
            {
                _context.Tareas.Remove(tarea);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult EditarTarea(int id)
        {
            var tarea = _context.Tareas.Find(id);

            if (tarea == null)
            {
                return NotFound();
            }

            return View(tarea);
        }

        [HttpPost]
        public IActionResult EditarTarea(Tarea tarea)
        {
            if (ModelState.IsValid)
            {
                _context.Tareas.Update(tarea);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tarea);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
