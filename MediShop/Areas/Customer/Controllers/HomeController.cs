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
        private readonly UserManager<ApplicationUser> _userManager;
        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index(string? search)
        {
            var medicines = _context.Medicines.Include(m => m.Supplier).AsQueryable();

            var lowerSearch = search?.ToLower();
            if (search != null)
            {
                medicines = medicines.Where(m => m.Name.Contains(lowerSearch) || m.Description.Contains(lowerSearch));
            }

            return View(medicines.ToList());
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
