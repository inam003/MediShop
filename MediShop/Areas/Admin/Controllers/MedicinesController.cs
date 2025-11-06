using MediShop.DataAccess.Data;
using MediShop.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MediShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MedicinesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public MedicinesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var medicines = _context.Medicines.Include(m => m.Supplier).ToList();
            return View(medicines);
        }

        public IActionResult Upsert(int? id)
        {
            Medicine? medicine = new Medicine();

            if (id == null || id == 0)
            {
                ViewBag.Suppliers = new SelectList(_context.Suppliers, "SupplierId", "Name");
                //return View(medicine);
            }
            else
            {
                medicine = _context.Medicines.Include(m => m.Supplier).FirstOrDefault(m => m.MedicineId == id);

                if (medicine == null)
                {
                    return NotFound();
                }

                ViewBag.Suppliers = new SelectList(_context.Suppliers, "SupplierId", "Name", medicine.SupplierId);
                return View(medicine);
            }

            return View(medicine);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Medicine medicine)
        {
            if (ModelState.IsValid)
            {
                if (medicine.MedicineId == 0)
                {
                    _context.Medicines.Add(medicine);
                }
                else
                {
                    _context.Medicines.Update(medicine);
                }

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Suppliers = new SelectList(_context.Suppliers.ToList(), "SupplierId", "Name", medicine.SupplierId);
            return View(medicine);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Medicine? medicine = _context.Medicines.Include(m => m.Supplier).FirstOrDefault(m => m.MedicineId == id);

            if (medicine == null)
            {
                return NotFound();
            }

            ViewBag.Suppliers = new SelectList(_context.Suppliers, "SupplierId", "Name", medicine.SupplierId);
            return View(medicine);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            Medicine? medicine = _context.Medicines.FirstOrDefault(m => m.MedicineId == id);
            if (medicine == null)
            {
                return NotFound();
            }
            _context.Medicines.Remove(medicine);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
