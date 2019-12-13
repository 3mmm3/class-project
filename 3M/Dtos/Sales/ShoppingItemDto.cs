﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3M.Dtos.Sales
{
    public class ShoppingItemDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Count { get; set; }
        public decimal ItemPrice => ProductPrice * Count;
    }
}
