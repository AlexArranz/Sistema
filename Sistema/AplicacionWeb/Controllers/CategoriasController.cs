using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AplicacionWeb.Models;

namespace AplicacionWeb.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly AplicacionWebContext _context;

        public CategoriasController(AplicacionWebContext context)
        {
            _context = context;
        }

        // GET: Categorias
        /// <summary>
        /// Vista que muestra el listado de todas las categorias de la base de datos
        /// </summary>
        /// <param name="sortOrder">Parametro de ordenacion</param>
        /// <returns>Retorna la vista index con el listado de las categorias</returns>
        public async Task<IActionResult> Index(String sortOrder)
        {
            ViewData["NombreSortParm"] = String.IsNullOrEmpty(sortOrder) ? "nombre_desc" : "";
            ViewData["DescripcionSortParm"] = sortOrder == "descripcion_asc" ? "descripcion_desc" : "descripcion_asc";

            var categorias = from s in _context.Categoria select s;

            switch (sortOrder)
            {
                case "nombre_desc":
                    categorias = categorias.OrderByDescending(s => s.Nombre);
                    break;
                case "descripcion_desc":
                    categorias = categorias.OrderByDescending(s => s.Descripcion);
                    break;
                case "descripcion_asc":
                    categorias = categorias.OrderBy(s => s.Descripcion);
                    break;
                default:
                    categorias = categorias.OrderBy(s => s.Nombre);
                    break;
            }

            return View(await categorias.AsNoTracking().ToListAsync());
            //return View(await _context.Categoria.ToListAsync());
        }

        // GET: Categorias/Details/5
        /// <summary>
        /// Vista que muestra los detalles del elemento seleccionado
        /// </summary>
        /// <param name="id">Identificador del elemento</param>
        /// <returns>Retorna la vista detalle de una categoria seleccionada</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria
                .SingleOrDefaultAsync(m => m.CategoriaID == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // GET: Categorias/Create
        /// <summary>
        /// Vista para crear una nueva categoria mediante un formulario
        /// </summary>
        /// <returns>Retorna la vista para crear un elemento nuevo</returns>
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Crea un elemento nuevo a partir de lo que recibe desde la vista create
        /// </summary>
        /// <param name="categoria">Objeto categoria</param>
        /// <returns>Retorna la vista index con los cambios si la accion se ha completado y si no recarga la misma pagina</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoriaID,Nombre,Descripcion,Estado")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        // GET: Categorias/Edit/5
        /// <summary>
        /// Vista para editar un elemento seleccionado de la lista
        /// </summary>
        /// <param name="id">Identificador del elemento</param>
        /// <returns>Retorna las vistas de error si algun dato es incorrecto y si no la vista para poder editar el elemento seleccionado</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria.SingleOrDefaultAsync(m => m.CategoriaID == id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Recibe todos los datos del formulario y actualiza la base de datos
        /// </summary>
        /// <param name="id">Elemento seleccionado</param>
        /// <param name="categoria">Categoria seleccionada</param>
        /// <returns>Retorna el index con los cambios realizados</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoriaID,Nombre,Descripcion,Estado")] Categoria categoria)
        {
            if (id != categoria.CategoriaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(categoria.CategoriaID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        // GET: Categorias/Delete/5
        /// <summary>
        /// Vista para borrar un registro seleccionado
        /// </summary>
        /// <param name="id">Elemento de la lista</param>
        /// <returns>Retorna la vista borrar para eliminar el registro seleccionado o la vista error si algun dato es incorrecto</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria
                .SingleOrDefaultAsync(m => m.CategoriaID == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // POST: Categorias/Delete/5
        /// <summary>
        /// Metodo que borra el elemento seleccionado
        /// </summary>
        /// <param name="id">Identificador del elemento</param>
        /// <returns>Retorla el index con los cambios realizados</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoria = await _context.Categoria.SingleOrDefaultAsync(m => m.CategoriaID == id);
            _context.Categoria.Remove(categoria);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categoria.Any(e => e.CategoriaID == id);
        }
    }
}
