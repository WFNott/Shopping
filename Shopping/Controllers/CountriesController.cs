using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Data;
using Shopping.Data.Entities;
using Shopping.Models;
using System.Diagnostics.Metrics;

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
                        View(await _context.countries.Include(c=>c.States).ToListAsync()) :
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
            var country = await _context.countries.Include(c => c.States)
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
            Country country = new()
            {
                States = new List<State>()
            };
            return View(country);
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
                
                try
                {
                    _context.Add(country);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un país con el mismo nombre.");
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
            return View(country);
        }




        // si el campo no es valido, se envia de nuevo a la pagina de crear pero manteniendo
        // la información digitad

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



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Country country)
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
                    return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un país con el mismo nombre.");
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
            return View(country);
        }

        public async Task<IActionResult> EditState(int? id)
        {
            if (id == null || _context.states == null)
            {
                return NotFound();
            }

            State states = await _context.states.Include(s=>s.Country).FirstOrDefaultAsync(s => s.Id==id);
            if (states == null)
            {
                return NotFound();
            }

            StateViewModel model = new()
            {

                CountryId = states.Country.Id,
                Id = states.Id,
                Name = states.Name

            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditState(int id, StateViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    State states = new()
                    {
                        Id = model.Id,
                        Name = model.Name,
                       
                    };

                    _context.Update(states);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new { Id = model.CountryId });

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un Departamento/Estado con el mismo nombre en este país.");
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
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> AddState(int? id) 
        { 

              if (id == null) 
              { 
                return NotFound();
              }

            Country country = await _context.countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            StateViewModel model = new()
            {
                CountryId = country.Id,
            };
        
            return View(model);
        }

        // POST: Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Debido a que este es un "Post" osea que se envian información se necesitan
        // parametros que determinen la información en este caso es country
        public async Task<IActionResult> AddState(StateViewModel model)
        {
            // si el modelo es valido osea si los campos estan llenos y no rompe ninguna
            // regla añade el pais y envia el cambio, redireccionandolo a Index

            if (ModelState.IsValid)
            {
                
                try
                {
                    // Se crea el objeto State que se añadira mas adelante en el Post
                    State state = new()
                    {
                        Cities = new List<City>(),
                        Country = await _context.countries.FindAsync(model.CountryId),
                        Name = model.Name,
                        
                    };

                    // le añado el objeto state anteriormente creado
                    _context.Add(state);
                    await _context.SaveChangesAsync();

                    // lo devolvemos a la pagina detalles segun el Id del pais donde se inserto el estado
                    return RedirectToAction(nameof(Details), new { Id = model.CountryId });

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un Departamento/Estado con el mismo país.");
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
            return View(nameof(Details), new { Id = model.CountryId });
        }




        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.countries == null)
            {
                return NotFound();
            }

            var country = await _context.countries.Include(c => c.States)
                .FirstOrDefaultAsync(c => c.Id == id);
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
