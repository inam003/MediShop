using MediShop.DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult SalesReport()
        {
            ViewBag.TotalSales = _context.Orders.Where(o => o.Status == "Delivered").Sum(o => o.TotalAmount);
            ViewBag.TotalOrders = _context.Orders.Where(o => o.Status == "Delivered").Count();

            var orderDetails = _context.Orders.Where(o => o.Status == "Delivered").ToList();
            return View(orderDetails);
        }

        public IActionResult StockReport()
        {
            var medicineDetails = _context.Medicines.ToList();
            return View(medicineDetails);
        }
    }
}
