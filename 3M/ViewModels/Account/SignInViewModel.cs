using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _3M.ViewModels.Account
{
    public class SignInViewModel
    {
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Display(Name = "رمز عبور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
