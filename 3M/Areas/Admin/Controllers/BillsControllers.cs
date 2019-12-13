using _3M.Areas.Admin.ViewModels;
using _3M.Constants;
using _3M.Repositories;
using _3M.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3M.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RoleConstants.Admin)]
    public class BillsControllers : Controller
    {
        private readonly BillRepository _billRepository;
        private readonly ProductRepository _productRepository;
        private readonly NotificationManager _notificationManager;

        public BillsControllers(BillRepository BillRepository,
            ProductRepository productRepository,
            NotificationManager notificationManager)
        {
            _billRepository = BillRepository;
            _productRepository = productRepository;
            _notificationManager = notificationManager;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var model = new BillListViewModel()
            {
                CurrentPage = page,
                PagesCount = await _billRepository.GetPagesCountAsync(20),
                Bills = await _billRepository.GetAllInvoices(page, 20),
                ItemsPerPage = 20,
            };
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Item(Guid id)
        {
            var bill = await _billRepository.GetInvoiceById(id);
            return View(bill);
        }

        public async Task<IActionResult> Proceed(Guid id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _billRepository.SetInvoiceProcessedState(id, true);
                    _notificationManager.Set(new Notification("سفارش با موفقیت ویرایش شد", Notification.SuccessType));
                }
                catch (Exception exception)
                {
                    _notificationManager.Set(new Notification(exception.Message, Notification.ErrorType));
                }
            }
            return RedirectToAction("Item", new { id });
        }


        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _billRepository.DeleteAsync(id);
                _notificationManager.Set(new Notification("سفارش با موفقیت حذف شد", Notification.SuccessType));
            }
            catch (Exception exception)
            {
                _notificationManager.Set(new Notification(exception.Message, Notification.ErrorType));
            }

            return RedirectToAction("Index");
        }
    }
}
