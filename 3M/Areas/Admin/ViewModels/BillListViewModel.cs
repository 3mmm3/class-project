using _3M.Dtos.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3M.Areas.Admin.ViewModels
{
    public class BillListViewModel
    {
        public IList<BillDto> Bills { get; set; }
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int PagesCount { get; set; }
    }
}
