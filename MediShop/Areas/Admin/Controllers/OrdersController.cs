using MediShop.DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var medicines = _context.Orders.ToList();
            return View(medicines);
        }

        [HttpPost]
        public IActionResult MarkCompleteStatus(int orderId) {
            var order = _context.Orders.Find(orderId);
            if (order != null)
            {
                order.Status = "Completed";
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult MarkDeliveredStatus(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            if (order != null)
            {
                order.Status = "Delivered";
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
