using _3M.Repositories;
using _3M.ViewModels.Sales;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3M.Services
{
    public class CartManager
    {
        private readonly ProductRepository _productRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartManager(ProductRepository productRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _productRepository = productRepository;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<List<CartItemViewModel>> GetCartAsync()
        {
            var cart = GetCartSession();
            var result = new List<CartItemViewModel>();
            foreach (var item in cart)
            {
                var product = await _productRepository.GetById(item.Key);
                CartItemViewModel cartItem;
                if (product == null)
                {
                    cartItem = new CartItemViewModel()
                    {
                        IsValid = false,
                        InvalidReason = "این محصول در سیستم ثبت نشده است",
                        ItemPrice = 0M,
                        Count = 0,
                        ProductPrice = 0M,
                        ProductId = item.Key,
                    };
                }
                else
                {
                    cartItem = new CartItemViewModel()
                    {
                        ProductId = product.Id,
                        ProductImage = product.ImageAddress,
                        ProductKey = product.Key,
                        ProductName = product.Name,
                        ProductPrice = product.Price,
                        Count = item.Value,
                        IsValid = true,
                        InvalidReason = string.Empty,
                        ItemPrice = product.Price * item.Value
                    };
                }

                result.Add(cartItem);
            }

            return result;
        }

        public void AddToCart(AddToCartViewModel model)
        {
            var cart = GetCartSession();
            if (cart.ContainsKey(model.ProductId))
                cart[model.ProductId] += model.Count;
            else
            {
                cart[model.ProductId] = model.Count;
            }
            SaveCartSession(cart);
        }

        public void RemoveFromCart(Guid productId)
        {
            var cart = GetCartSession();
            if (cart.ContainsKey(productId))
                cart.Remove(productId);
            SaveCartSession(cart);
        }

        public void ClearCart()
        {
            _httpContextAccessor.HttpContext.Session.Remove("cart");
        }

        private Dictionary<Guid, int> GetCartSession()
        {
            Dictionary<Guid, int> cart;
            try
            {
                var cartSerialized = _httpContextAccessor.HttpContext.Session.GetString("cart");
                cart = JsonConvert.DeserializeObject<Dictionary<Guid, int>>(cartSerialized);
            }
            catch
            {
                cart = new Dictionary<Guid, int>();
            }

            return cart;
        }

        private void SaveCartSession(Dictionary<Guid, int> cart)
        {
            var cartSerialized = JsonConvert.SerializeObject(cart);
            _httpContextAccessor.HttpContext.Session.SetString("cart", cartSerialized);
        }
    }
}
