using MediShop.DataAccess.Data;
using MediShop.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public HomeController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var medicines = _context.Medicines.ToList();
            return View(medicines);
        }

        public IActionResult MedicineDetails(int id)
        {
            var medicine = _context.Medicines.Include(m => m.Supplier).FirstOrDefault(m => m.MedicineId == id);
            if (medicine == null)
            {
                return NotFound();
            }

            return View(medicine);
        }
    }
}
