using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _3M.Dtos.Sales
{
    public class BillDto
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        [Display(Name = "سفارش دهنده")]
        public string CustomerName { get; set; }

        [Display(Name = "تحویل گیرنده")]

        public string RecipientName { get; set; }

        [Display(Name = "استان")]
        public string Province { get; set; }

        [Display(Name = "شهر")]
        public string City { get; set; }

        [Display(Name = "آدرس")]
        public string Address { get; set; }

        [Display(Name = "شماره تماس")]
        public string Tel { get; set; }

        [Display(Name = "کد پستی")]
        public string PostalCode { get; set; }

        public List<ShoppingItemDto> ShoppingCart { get; set; }

        public bool IsPaid { get; set; }
        public bool IsProcessed { get; set; }
        public DateTime RegisterDate { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
