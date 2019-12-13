using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3M.ViewModels.Sales.Validators
{
    public class UpdateInvoiceAddressViewModelValidator : AbstractValidator<UpdateBillAddressViewModel>
    {
        public UpdateInvoiceAddressViewModelValidator()
        {
            RuleFor(i => i.Address)
                .NotNull().WithMessage("لطفا آدرس را وارد کنید")
                .NotEmpty().WithMessage("لطفا آدرس را وارد کنید");
            RuleFor(i => i.City)
                .NotNull().WithMessage("لطفا شهر را وارد کنید")
                .NotEmpty().WithMessage("لطفا شهر را وارد کنید");
            RuleFor(i => i.Province)
                .NotNull().WithMessage("لطفا استان را وارد کنید")
                .NotEmpty().WithMessage("لطفا استان را وارد کنید");
            RuleFor(i => i.PostalCode)
                .NotNull().WithMessage("لطفا کد پستی را وارد کنید")
                .NotEmpty().WithMessage("لطفا کد پستی را وارد کنید");
            RuleFor(i => i.RecipientName)
                .NotNull().WithMessage("لطفا نام تحویل گیرنده را وارد کنید")
                .NotEmpty().WithMessage("لطفا نام تحویل گیرنده را وارد کنید");
            RuleFor(i => i.Tel)
                .NotNull().WithMessage("لطفا شماره تماس را وارد کنید")
                .NotEmpty().WithMessage("لطفا شماره تماس را وارد کنید");

        }
    }
}
