using MediShop.DataAccess.Data;
using MediShop.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public CartController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var cartItems = _context.CartItems
                .Include(m => m.Medicine)
                .Where(c => c.UserId == userId)
                .ToList();

            return View(cartItems);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int medicineId, int quantity)
        {
            var userId = _userManager.GetUserId(User);
            var existingCartItem = _context.CartItems.FirstOrDefault(c => c.UserId == userId && c.MedicineId == medicineId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += quantity;
            }
            else
            {
                var cart = new Cart
                {
                    UserId = userId,
                    MedicineId = medicineId,
                    Quantity = quantity
                };

                _context.CartItems.Add(cart);
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Cart", new { area = "Customer" });
        }

        [HttpPost]
        public IActionResult IncreaseQuantity(int cartId) 
        {
            var cartItem = _context.CartItems.Find(cartId);
            if (cartItem != null)
            {
                cartItem.Quantity++;
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DecreaseQuantity(int cartId)
        {
            var cartItem = _context.CartItems.Find(cartId);
            if (cartItem != null && cartItem.Quantity > 1)
            {
                cartItem.Quantity--;
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Remove(int cartId)
        {
            var cartItem = _context.CartItems.Find(cartId);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
