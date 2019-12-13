using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _3M.DbContexts;
using _3M.Repositories;
using _3M.ViewModels;
using _3M.ViewModels.Products;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace _3M.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly CategoryRepository _categoryRepository;

        public ProductsController(ProductRepository productRepository, CategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index(string category = null, string search = null, int page = 1)
        {
            var model = new ProductsListViewModel()
            {
                Category = await _categoryRepository.GetByKeyAsync(category),
                Products = await _productRepository.FindAsync(category, search, page, 10),
                PagesCount = await _productRepository.GetPagesCountAsync(category, search, 10),
                CurrentPage = page,
            };
            return View(model);
        }

        [Route("[controller]/[action]/{product}")]
        public async Task<IActionResult> Details(string product)
        {
            var model = await _productRepository.GetByKey(product);
            if (model == null)
                return NotFound();
            return View(model);
        }
    }
}