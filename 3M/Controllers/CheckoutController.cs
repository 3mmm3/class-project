using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _3M.Dtos.Sales;
using _3M.Repositories;
using _3M.Services;
using _3M.ViewModels.Sales;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using _3M.ViewModels;

namespace _3M.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly CartManager _cartManager;
        private readonly UserprofileRepository _userProfileRepository;
        private readonly BillRepository _BillRepository;
        private readonly NotificationManager _notificationManager;

        public CheckoutController(CartManager cartManager,
            UserprofileRepository userProfileRepository,
            BillRepository billRepository,
            NotificationManager notificationManager)
        {
            _cartManager = cartManager;
            _userProfileRepository = userProfileRepository;
            _BillRepository = billRepository;
            _notificationManager = notificationManager;
        }

        public async Task<IActionResult> Index()
        {
            var bill = await CreateInvoice(null);
            return View(bill);
        }

        [HttpPost]
        public async Task<IActionResult> Index(UpdateBillAddressViewModel model)
        {
            var bill = await CreateInvoice(model);

            if (ModelState.IsValid)
            {
                await _BillRepository.AddBill(bill);
                _cartManager.ClearCart();

                // go to payment gateway

                await _BillRepository.SetInvoicePaidState(bill.Id, true);
                _notificationManager.Set(new Notification("سفارش شما با موفقیت ثبت گردید.", Notification.SuccessType));

                return Redirect("/");
            }

            return View(bill);
        }

        [NonAction]
        private async Task<BillDto> CreateInvoice(UpdateBillAddressViewModel addressInfo)
        {
            var userIdClaim = User.Claims.FirstOrDefault(i => i.Type.Contains("nameidentifier"));
            var cart = (await _cartManager.GetCartAsync()).Where(i => i.IsValid).ToList();
            var userProfile = await _userProfileRepository.GetUserProfileAsync(Guid.Parse(userIdClaim.Value));
            var model = new BillDto()
            {
                Id = Guid.NewGuid(),
                Tel = userProfile.Number,
                IsPaid = false,
                IsProcessed = false,
                Address = userProfile.Address,
                City = userProfile.City,
                Province = userProfile.Province,
                CustomerId = userProfile.UserId,
                CustomerName = $"{userProfile.FirstName} {userProfile.LastName}",
                RecipientName = $"{userProfile.FirstName} {userProfile.LastName}",
                PostalCode = userProfile.PostalCode,
                RegisterDate = DateTime.Now,
                ShoppingCart = cart.Select(i => new ShoppingItemDto()
                {
                    Id = Guid.NewGuid(),
                    Count = i.Count,
                    ProductPrice = i.ProductPrice,
                    ProductName = i.ProductName,
                    ProductId = i.ProductId
                }).ToList(),
                TotalPrice = cart.Any() ? cart.Sum(i => i.ItemPrice) : 0M,
            };
            if (addressInfo != null)
            {
                model.Address = addressInfo.Address;
                model.Tel = addressInfo.Tel;
                model.PostalCode = addressInfo.PostalCode;
                model.City = addressInfo.City;
                model.Province = addressInfo.Province;
                model.RecipientName = addressInfo.RecipientName;
            }

            return model;
        }
    }
}