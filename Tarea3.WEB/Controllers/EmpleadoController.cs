using Microsoft.AspNetCore.Mvc;
using Tarea3.DA.Repositorios;
using Tarea3.MODELS;

namespace Tarea3.WEB.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly IEmpleadoRepository _ropository;
        private const int TAMANO_MAX_PAGINAS = 5;
        private const int TAMANO_MIN_PAGINAS = 1;
        private const int SIN_PAGINAS = 0;

        public EmpleadoController(IEmpleadoRepository repository)
        {
            _ropository = repository;
        }
        public IActionResult Index(string? busqueda = "", int pagina = 1)
        {
            if (pagina > TAMANO_MIN_PAGINAS) pagina = TAMANO_MIN_PAGINAS;

            var empleado = _ropository.ObtenerPaginado(pagina, TAMANO_MAX_PAGINAS, busqueda);
            var totalRegistros = _ropository.ContarTotalEmpleados(busqueda);
            var totalPaginas = (int)Math.Ceiling((double) totalRegistros / TAMANO_MAX_PAGINAS);

            if (totalPaginas == SIN_PAGINAS) totalPaginas = TAMANO_MIN_PAGINAS;
            if (pagina > totalPaginas) pagina = totalPaginas;

            ViewBag.Busqueda = busqueda;
            ViewBag.PaginaActual = pagina;
            ViewBag.TotalPaginas = totalPaginas;
            ViewBag.TotalRegistros = totalRegistros;


            return View(empleado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Empleado empleado)
        {
            if (!ModelState.IsValid)
            {
                return View(empleado);
            }

            _ropository.Agregar(empleado);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var empleado = _ropository.ObtenerPorId(id);

            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Empleado empleado)
        {
            if (id != empleado.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(empleado);
            }

            var existente = _ropository.ObtenerPorId(id);

            if (existente == null)
            {
                return NotFound();
            }

            empleado.FechaIngreso = existente.FechaIngreso;
            _ropository.Actualizar(empleado);

            return RedirectToAction(nameof(Index), new { busqueda = "" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleActivo(int id, string? busqueda = "", int pagina = 1)
        {
            var empleado = _ropository.ObtenerPorId(id);

            if (empleado == null)
            {
                return NotFound();
            }

            empleado.Activo = !empleado.Activo;
            _ropository.Actualizar(empleado);

            return RedirectToAction(nameof(Index), new { busqueda, pagina });
        }
    }
}
