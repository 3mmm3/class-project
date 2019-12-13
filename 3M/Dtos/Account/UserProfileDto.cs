using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _3M.Dtos.Account
{
    public class UserProfileDto
    {
        public Guid UserId { get; set; }

        [Display(Name = "نام")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [Display(Name = "استان")]
        public string Province { get; set; }

        [Display(Name = "شهر")]
        public string City { get; set; }

        [Display(Name = "آدرس")]
        public string Address { get; set; }

        [Display(Name = "شماره تماس")]
        public string Number { get; set; }

        [Display(Name = "کد پستی")]
        public string PostalCode { get; set; }
    }
}
