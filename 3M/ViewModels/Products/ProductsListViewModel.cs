using _3M.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3M.ViewModels.Products
{
    public class ProductsListViewModel
    {
        public CategoryDto Category { get; set; }
        public IList<ProductDto> Products { get; set; }
        public int CurrentPage { get; set; }
        public int PagesCount { get; set; }
    }
}
