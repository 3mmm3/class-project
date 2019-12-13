using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _3M.ViewModels.Account
{
    public class SignupViewModel
    {
        [Display(Name = "نام")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Display(Name = "رمز عبور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تکرار رمز عبور")]
        [DataType(DataType.Password)]
        public string RetypePassword { get; set; }

        [Display(Name = "ایمیل")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "شماره تماس")]
        public string Tel { get; set; }
    }
}
