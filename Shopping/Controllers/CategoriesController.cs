using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Data;
using Shopping.Data.Entities;

namespace Shopping.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly DataContex _context;

        public CategoriesController(DataContex context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return _context.categories != null ? View(await _context.categories.ToListAsync()) : Problem("La entidad 'Categoria' no puede ser nula.");
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Debido a que este es un "Post" osea que se envian información se necesitan
        // parametros que determinen la información en este caso es country
        public async Task<IActionResult> Create(Category category)
        {
            // si el modelo es valido osea si los campos estan llenos y no rompe ninguna
            // regla añade el pais y envia el cambio, redireccionandolo a Index

            if (ModelState.IsValid)
            {
                _context.Add(category);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una categoria con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(category);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.categories == null)
            {
                return NotFound();
            }

            Category category = await _context.categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una categoria con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }

            }
            return View(category);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.categories == null)
            {
                return NotFound();
            }

            Category category = await _context.categories.FirstOrDefaultAsync(m => m.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.categories == null)
            {
                return Problem("Entity set 'DataContex.categories'  is null.");
            }

            Category category = await _context.categories.FindAsync(id);
            if (category != null)
            {
                _context.categories.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            // Aqui le dice que si el id es nulo o no hay un registro en la entidad countries
            // mandelo a una pagina personalizada llamada "NotFound"
            if (id == null || _context.categories == null)
            {
                return NotFound();
            }

            /* Manda una peticion para obtener el country que tenga el mismo id que el 
             digitado, en caso de que no exista lo envia a "NotFound", si existe le 
             muetsra el pais*/
            Category category = await _context.categories.FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
    }
}
