using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3M.DataModels.Sales
{
    public class Bill
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string RecipientName { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        public string PostalCode { get; set; }
        public List<ShoppingItem> ShoppingCart { get; set; }

        public DateTime RegisterDate { get; set; }
        public bool IsPaid { get; set; }
        public bool IsProcessed { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
