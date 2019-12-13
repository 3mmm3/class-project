using _3M.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3M.Areas.Admin.ViewModels
{
    public class CategoryListViewModel
    {
        public IList<CategoryDto> Categories { get; set; }
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int PagesCount { get; set; }
    }
}
