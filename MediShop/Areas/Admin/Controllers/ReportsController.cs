using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ReportsController : Controller
    {
        public IActionResult SalesReport()
        {
            return View();
        }

        public IActionResult StockReport()
        {
            return View();
        }
    }
}
