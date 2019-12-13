using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3M.ViewModels.Sales
{
    public class AddToCartViewModel
    {
        public Guid ProductId { get; set; }
        public int Count { get; set; }
    }
}
