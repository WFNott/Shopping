using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shopping.Data;
using Shopping.Data.Entities;

namespace Shopping.Controllers
{
    // El controlador es una clase que herada de una clase mas grande llamada 
    // Controller, esta clase es propia del framework Entitys
    public class CountriesController : Controller
    {
        // Se crea un atributo privado llamado _context, el cual tiene toda la
        // información del contexto de datos
        private readonly DataContex _context;

        // Se inyecta información a traves del parametro "context"
        // por eso es de tipo DataContex, el caul trae la conexión.
        public CountriesController(DataContex context)
        {
            _context = context;
        }

        // GET: Countries a traves del metodo Index siempre y cuando cumpla el requisito de la interfaz
        public async Task<IActionResult> Index()
        {
            // En caso de que el pais no tenga un valor nulo me mostrara los paises
              return _context.countries != null ?

                          // _context.countries.ToListAsync() = Select * From countries
                          View(await _context.countries.ToListAsync()) :
                          // en caso de que pais tenga un valor nulo, mostrara este error
                          Problem("Entity set 'DataContex.countries'  is null.");
        }

        // GET: Countries/Details/5, el "int? id" se refiere a que puede o no recibir un id
        public async Task<IActionResult> Details(int? id)
        {
            // Aqui le dice que si el id es nulo o no hay un registro en la entidad countries
            // mandelo a una pagina personalizada llamada "NotFound"
            if (id == null || _context.countries == null)
            {
                return NotFound();
            }

            /* Manda una peticion para obtener el country que tenga el mismo id que el 
             digitado, en caso de que no exista lo envia a "NotFound", si existe le 
             muetsra el pais*/
            var country = await _context.countries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: Countries/Create se trae la pagina para mostrar al usuario

        [HttpGet]
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
        public async Task<IActionResult> Create(Country country)
        {
            // si el modelo es valido osea si los campos estan llenos y no rompe ninguna
            // regla añade el pais y envia el cambio, redireccionandolo a Index
            if (ModelState.IsValid)
            {
                _context.Add(country);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // si el campo no es valido, se envia de nuevo a la pagina de crear pero manteniendo
            // la información digitada

            return View(country);
        }

        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.countries == null)
            {
                return NotFound();
            }

            var country = await _context.countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Country country)
        {
            if (id != country.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(country);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(country.Id))
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
            return View(country);
        }

        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.countries == null)
            {
                return NotFound();
            }

            var country = await _context.countries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.countries == null)
            {
                return Problem("Entity set 'DataContex.countries'  is null.");
            }
            var country = await _context.countries.FindAsync(id);
            if (country != null)
            {
                _context.countries.Remove(country);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CountryExists(int id)
        {
          return (_context.countries?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
