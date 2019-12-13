using _3M.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3M.Components
{
    public class TopMenuViewComponent : ViewComponent
    {
        private readonly CategoryRepository _categoryRepository;

        public TopMenuViewComponent(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryRepository.GetAllAsync(1, 1000);
            return View(categories);
        }
    }
}
