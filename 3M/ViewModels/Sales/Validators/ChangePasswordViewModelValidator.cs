using _3M.ViewModels.Account;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3M.ViewModels.Validators
{
    public class ChangePasswordViewModelValidator : AbstractValidator<ChangePasswordViewModel>
    {
        public ChangePasswordViewModelValidator()
        {
            RuleFor(i => i.CurrentPassword)
                .NotNull().WithMessage("رمز عبور فعلی نمی تواند خالی باشد")
                .NotEmpty().WithMessage("رمز عبور فعلی نمی تواند خالی باشد")
                .MinimumLength(5).WithMessage("طول رمز عبور فعلی باید بیشتر از 5 حرف باشد");

            RuleFor(i => i.NewPassword)
              .NotNull().WithMessage("رمز عبور جدید نمی تواند خالی باشد")
              .NotEmpty().WithMessage("رمز عبور جدید نمی تواند خالی باشد")
              .MinimumLength(5).WithMessage("طول رمز عبور جدید باید بیشتر از 5 حرف باشد");

            RuleFor(i => i.ConfirmNewPassword)
              .NotNull().WithMessage("تکرار رمز عبور جدید نمی تواند خالی باشد")
              .NotEmpty().WithMessage("تکرار رمز عبور جدید نمی تواند خالی باشد")
              .MinimumLength(5).WithMessage("طول تکرار رمز عبور جدید باید بیشتر از 5 حرف باشد")
              .Must((m, cnp) => cnp == m.NewPassword).WithMessage("رمز عبور جدید با تکرار آن مطابقت ندارد");
        }
    }
}
