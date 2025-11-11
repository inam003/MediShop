using MediShop.DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var medicines = _context.Orders
                .Include(o => o.User)
                .Include(od => od.OrderDetails)
                .ToList();
            return View(medicines);
        }

        [HttpPost]
        public IActionResult MarkCompleteStatus(int orderId) {
            var order = _context.Orders.Find(orderId);
            if (order == null)
            {
                TempData["Error"] = "Order not found.";
                return NotFound();
            }
            else
            {
                order.Status = "Completed";
            }

            var orderDetails = _context.OrderDetails.Where(od => od.OrderId == orderId).ToList();

            foreach (var detail in orderDetails)
            {
                var medicine = _context.Medicines.Find(detail.MedicineId);
                if (medicine != null)
                {
                    if(medicine.StockQuantity >= detail.Quantity){
                        medicine.StockQuantity -= detail.Quantity;
                    }
                    else
                    {
                        TempData["Error"] = $"Insufficient stock for medicine ID {medicine.MedicineId}.";
                    }
                }
            }

            _context.SaveChanges();
            TempData["Success"] = "Order Completed successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult MarkDeliveredStatus(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            if (order != null)
            {
                order.Status = "Delivered";
            }

            _context.SaveChanges();
            TempData["Success"] = "Order Delivered successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
