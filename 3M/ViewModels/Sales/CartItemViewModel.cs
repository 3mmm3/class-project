using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3M.ViewModels.Sales
{
    public class CartItemViewModel
    {
        public Guid ProductId { get; set; }
        public string ProductKey { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImage { get; set; }
        public decimal ItemPrice { get; set; }
        public int Count { get; set; }
        public bool IsValid { get; set; }
        public string InvalidReason { get; set; }
    }
}
