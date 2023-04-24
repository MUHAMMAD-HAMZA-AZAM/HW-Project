using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HW.IdentityViewModels
{
    public class GetQuotationValidator : AbstractValidator<GetQuotationVM>
    {
        public GetQuotationValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Category).NotEmpty().WithMessage("Select option").Must(p => p > 0);
            //RuleFor(x => x.SubCategory).NotEmpty().WithMessage("Select option").Must(p => p > 0);
            RuleFor(x => x.City).NotEmpty().WithMessage("Select option");
            RuleFor(x => x.JobTitle).NotEmpty().WithMessage("Enter your Job Title");
            RuleFor(x => x.JobDescription).NotEmpty().WithMessage("Enter your Job Description");
            RuleFor(x => x.Location).NotEmpty().WithMessage("Give Your Location");
            RuleFor(x => x.Town).NotEmpty().WithMessage("Enter your town name");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Enter your address");
        }

    }
}
