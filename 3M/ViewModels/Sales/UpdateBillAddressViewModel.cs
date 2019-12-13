using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3M.ViewModels.Sales
{
    public class UpdateBillAddressViewModel
    {
        public Guid InvoiceId { get; set; }
        public string RecipientName { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        public string PostalCode { get; set; }
    }
}
