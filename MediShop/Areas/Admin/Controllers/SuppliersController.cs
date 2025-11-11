using MediShop.DataAccess.Data;
using MediShop.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                    TempData["Error"] = "Supplier not found.";
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
                    TempData["Success"] = "Supplier created successfully!";
                    _context.Suppliers.Add(supplier);
                }
                else
                {
                    TempData["Success"] = "Supplier updated successfully!";
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
                TempData["Error"] = "Invalid supplier ID.";
                return NotFound();
            }

            Supplier? supplier = _context.Suppliers.FirstOrDefault(m => m.SupplierId == id);

            if (supplier == null)
            {
                TempData["Error"] = "Supplier not found.";
                return NotFound();
            }

            return View(supplier);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            Supplier? supplier = _context.Suppliers.Include(s => s.Medicines).FirstOrDefault(m => m.SupplierId == id);

            if (supplier != null && supplier.Medicines.Any())
            {
                TempData["Error"] = "Cannot delete supplier with associated medicines. Please remove associated medicines first.";
                return RedirectToAction(nameof(Index));
            }

            _context.Suppliers.Remove(supplier);
            _context.SaveChanges();
            TempData["Success"] = "Supplier deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
