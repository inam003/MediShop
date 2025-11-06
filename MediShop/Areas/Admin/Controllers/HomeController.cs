using MediShop.DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.TotalMedicines = _context.Medicines.Count();
            //ViewBag.TotalOrders = _context.Orders.Count();
            //ViewBag.TotalCustomers = _context.Customers.Count();
            ViewBag.TotalSuppliers = _context.Suppliers.Count();

            return View();
        }
    }
}
