using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _3M.ViewModels.Account
{
    public class ChangePasswordViewModel
    {

        [Display(Name = "رمز عبور فعلی")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Display(Name = "رمز عبور جدید")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "تکرار رمز عبور جدید")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
    }
}
