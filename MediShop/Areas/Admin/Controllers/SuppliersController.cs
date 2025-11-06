using MediShop.DataAccess.Data;
using MediShop.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SuppliersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SuppliersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var suppliers = _context.Suppliers.ToList();
            return View(suppliers);
        }

        public IActionResult Upsert(int? id)
        {
            Supplier supplier = new Supplier();
            if (id == null || id == 0)
            {
                return View(supplier);
            }
            else
            {
                supplier = _context.Suppliers.FirstOrDefault(s => s.SupplierId == id);
                if(supplier == null)
                {
                    return NotFound();
                }

                return View(supplier);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                if(supplier.SupplierId == 0)
                {
                    _context.Suppliers.Add(supplier);
                }
                else
                {
                    _context.Suppliers.Update(supplier);
                }

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Supplier? supplier = _context.Suppliers.FirstOrDefault(m => m.SupplierId == id);

            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            Supplier? supplier = _context.Suppliers.FirstOrDefault(m => m.SupplierId == id);
            if (supplier == null)
            {
                return NotFound();
            }
            _context.Suppliers.Remove(supplier);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
