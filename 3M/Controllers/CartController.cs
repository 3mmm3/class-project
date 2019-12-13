using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _3M.Services;
using _3M.ViewModels.Sales;
using Microsoft.AspNetCore.Mvc;

namespace _3M.Controllers
{
    public class CartController : Controller
    {
        private readonly CartManager _cartManager;

        public CartController(CartManager cartManager)
        {
            _cartManager = cartManager;
        }

        public async Task<IActionResult> Index()
        {
            var result = new CartListViewModel()
            {
                CartItems = await _cartManager.GetCartAsync(),
            };

            if (result.CartItems.Any())
                result.TotalPrice = result.CartItems.Sum(i => i.ItemPrice);

            return View(result);
        }

        [HttpPost]
        public IActionResult AddToCart(AddToCartViewModel model)
        {
            if (ModelState.IsValid)
                _cartManager.AddToCart(model);

            return RedirectToAction("Index");
        }

        public IActionResult Remove(Guid productId)
        {
            _cartManager.RemoveFromCart(productId);
            return RedirectToAction("Index");
        }
    }
}