using _3M.DataModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3M.DataModels
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageAddress { get; set; }
        public decimal Price { get; set; }
        public ICollection<Property> Properties { get; set; }
        public Category Category { get; set; }

    }

}
