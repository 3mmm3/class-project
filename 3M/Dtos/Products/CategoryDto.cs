using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _3M.Dtos.Products
{
    public class CategoryDto
    {
        [Display(Name = "شناسه")]
        public Guid Id { get; set; }

        [Display(Name = "کلید")]
        public string Key { get; set; }

        [Display(Name = "نام")]
        public string Name { get; set; }
    }
}
