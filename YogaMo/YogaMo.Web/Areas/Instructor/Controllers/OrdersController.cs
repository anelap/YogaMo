using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YogaMo.Web.Helpers;
using YogaMo.WebAPI.Database;

namespace YogaMo.Web.Areas.Instructor.Controllers
{
    [Area("Instructor")]
    [Authorization(isInstructor: true)]
    public class OrdersController : Controller
    {
        private readonly _150222Context _context;

        public OrdersController(_150222Context context)
        {
            _context = context;
        }

        // GET: Instructor/Orders
        public async Task<IActionResult> Index(OrderStatus? OrderStatus)
        {
            var query = _context.Order.Include(o => o.Client).OrderByDescending(x=>x.Date).AsQueryable();
            if (OrderStatus != null)
                query = query.Where(x => x.OrderStatus == OrderStatus.Value);

            ViewBag.OrderStatus = OrderStatus;

            return View(await query.ToListAsync());
        }

        // GET: Instructor/Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Client)
                .Include(o => o.OrderItem)
                .Include("OrderItem.Product")
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public async Task<IActionResult> MarkAsCompleted(int Id)
        {
            var Order = await _context.Order.FirstOrDefaultAsync(m => m.OrderId == Id);
            if(Order != null && Order.OrderStatus == OrderStatus.Confirmed)
            {
                Order.OrderStatus = OrderStatus.Completed;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index), new { OrderStatus = Model.OrderStatus.Confirmed });
        }
        public async Task<IActionResult> CancelOrder(int Id)
        {
            var Order = await _context.Order.FirstOrDefaultAsync(m => m.OrderId == Id);
            if(Order != null && Order.OrderStatus == OrderStatus.Confirmed)
            {
                Order.OrderStatus = OrderStatus.Canceled;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index), new { OrderStatus = Model.OrderStatus.Confirmed });
        }
    }
}
