using Microsoft.AspNetCore.Mvc;
using SA_Online_Mart.Context;
using SA_Online_Mart.Models.Cart;
using System.Text.Json;

namespace SA_Online_Mart.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        public IActionResult AddToCart(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                var cart = GetCart();
                cart.AddToCart(product);
                SaveCart(cart);
            }

            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int id)
        {
            var cart = GetCart();
            cart.RemoveFromCart(id);
            SaveCart(cart);

            return RedirectToAction("Index");
        }

        private Cart GetCart()
        {
            var json = HttpContext.Session.GetString("Cart");
            return string.IsNullOrEmpty(json) ? new Cart() : JsonSerializer.Deserialize<Cart>(json);
        }

        private void SaveCart(Cart cart)
        {
            var json = JsonSerializer.Serialize(cart);
            HttpContext.Session.SetString("Cart", json);
        }
        
        public IActionResult Checkout()
        {
            var cart = GetCart();

            if (cart.Items.Count == 0)
            {
                ModelState.AddModelError("", "There are no items in the cart.");
            }
        
            return View(cart);
        }
    }
}


 