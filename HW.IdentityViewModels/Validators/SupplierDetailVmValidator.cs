using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HW.IdentityViewModels
{
    public class SupplierDetailVmValidator : AbstractValidator<SupplierBusinessDetailVM>
    {
        public SupplierDetailVmValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.SupplierId).NotEmpty();
            RuleFor(x => x.CompanyName).NotEmpty();
            //RuleFor(x => x.CompanyRegistrationNo).NotEmpty();
            RuleFor(x => x.PrimaryTradeId).NotEmpty().WithMessage("Select an option");
            RuleFor(x => x.ProductIds).NotEmpty().WithMessage("Provide at least one product");
            RuleFor(x => x.DeliveryRadius).NotEmpty().WithMessage("Select an option");
            RuleFor(x => x.CityId).NotEmpty().WithMessage("Select an option");
            RuleFor(x => x.BusinessAddress).NotEmpty().WithMessage("Enter your business address");

        }
    }
}
