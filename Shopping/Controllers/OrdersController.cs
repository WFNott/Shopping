using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Data;
using Shopping.Data.Entities;
using Shopping.Enum;
using Vereyon.Web;

namespace Shopping.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        private readonly DataContex _context;
        private readonly IFlashMessage _flashMessage;

        public OrdersController(DataContex context, IFlashMessage flashMessage)
        {
            _context = context;
            _flashMessage = flashMessage;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sales
                .Include(s => s.User)
                .Include(s => s.SaleDetails)
                .ThenInclude(sd => sd.Product)
                .ToListAsync());
        }
        public async Task<IActionResult> Details(int? id )
        {
            if (id == null)
            {
                return NotFound();
            }

            Sale sale = await _context.Sales
                .Include(s => s.User)
                .Include(s => s.SaleDetails)
                .ThenInclude(sd => sd.Product)
                .ThenInclude(p => p.ProductImages)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        public async Task<IActionResult> Dispatch(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Sale sale = await _context.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }

            if (sale.OrderStatus != OrderStatus.Nuevo)
            {
                _flashMessage.Danger("Solo se pueden despachar pedidos que estén en estado 'nuevo'.");
            }
            else
            {
                sale.OrderStatus = OrderStatus.Despachado;
                _context.Sales.Update(sale);
                await _context.SaveChangesAsync();
                _flashMessage.Confirmation("El estado del pedido ha sido cambiado a 'despachado'.");
            }

            return RedirectToAction(nameof(Details), new { Id = sale.Id });
        }

    }

}
