using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3M.ViewModels.Sales
{
    public class CartListViewModel
    {
        public List<CartItemViewModel> CartItems { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
