using _3M.Areas.Admin.ViewModels;
using _3M.Constants;
using _3M.Dtos.Products;
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
    public class CategoryControllers : Controller
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly NotificationManager _notificationManager;

        public CategoryControllers(CategoryRepository categoryRepository, NotificationManager notificationManager)
        {
            _categoryRepository = categoryRepository;
            _notificationManager = notificationManager;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var model = new CategoryListViewModel()
            {
                CurrentPage = page,
                PagesCount = await _categoryRepository.GetPagesCountAsync(20),
                Categories = await _categoryRepository.GetAllAsync(page, 20),
                ItemsPerPage = 20,
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CategoryDto();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryRepository.InsertAsync(model);
                    _notificationManager.Set(new Notification("دسته بندی با موفقیت ثبت شد", Notification.SuccessType));
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    _notificationManager.Set(new Notification(exception.Message, Notification.ErrorType));
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var model = await _categoryRepository.GetByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CategoryDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryRepository.UpdateAsync(model);
                    _notificationManager.Set(new Notification("دسته بندی با موفقیت ویرایش شد", Notification.SuccessType));
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    _notificationManager.Set(new Notification(exception.Message, Notification.ErrorType));
                }
            }
            return View(model);
        }


        public async Task<IActionResult> Delete(Guid id)
        {

            try
            {
                await _categoryRepository.DeleteAsync(id);
                _notificationManager.Set(new Notification("دسته بندی با موفقیت حذف شد", Notification.SuccessType));
            }
            catch (Exception exception)
            {
                _notificationManager.Set(new Notification(exception.Message, Notification.ErrorType));
            }

            return RedirectToAction("Index");
        }


    }
}
