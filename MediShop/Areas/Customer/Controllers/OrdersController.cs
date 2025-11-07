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
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public OrdersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var orders = _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(d => d.Medicine)
                .Where(o => o.UserId == userId)
                .ToList();
            return View(orders);
        }

        public IActionResult Checkout()
        {
            var userId = _userManager.GetUserId(User);
            var cartItems = _context.CartItems
                .Include(m => m.Medicine)
                .Where(c => c.UserId == userId)
                .ToList();

            if(!cartItems.Any())
            {
                return RedirectToAction("Index", "Cart");
            }

            var totalAmount = cartItems.Sum(item => item.Medicine.Price * item.Quantity);
            ViewBag.TotalAmount = totalAmount;

            return View(cartItems);
        }

        public IActionResult PlaceOrder()
        {
            var userId = _userManager.GetUserId(User);
            var cartItems = _context.CartItems
                .Include(m => m.Medicine)
                .Where(c => c.UserId == userId)
                .ToList();

            if (!cartItems.Any())
            {
                return RedirectToAction("Index", "Cart");
            }

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                Status = "Pending",
                TotalAmount = cartItems.Sum(item => item.Medicine.Price * item.Quantity),
            };
            _context.Orders.Add(order);
            _context.SaveChanges();

            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    OrderId = order.OrderId,
                    MedicineId = item.MedicineId,
                    Quantity = item.Quantity,
                    Price = item.Medicine.Price
                };
                _context.OrderDetails.Add(orderDetail);
            }
            _context.SaveChanges();

            _context.CartItems.RemoveRange(cartItems);
            _context.SaveChanges();

            return RedirectToAction("OrderConfirmation", new { id = order.OrderId });
        }

        public IActionResult OrderConfirmation(int id)
        {
            var order = _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(d => d.Medicine)
                .FirstOrDefault(o => o.OrderId == id);

            if (order == null)
                return NotFound();

            return View(order);
        }
    }
}
