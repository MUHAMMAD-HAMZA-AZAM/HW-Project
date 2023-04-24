using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace HW.UserViewModels.Validators
{
    public class QuotationDetailVmValidator : AbstractValidator<JobQuotationDetailVM>
    {
        public QuotationDetailVmValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            //RuleFor(x => x.SubSkillId).NotEmpty().GreaterThan(0).WithMessage("Select an option");
            RuleFor(x => x.QuotesQuantity).NotEmpty().GreaterThan(0).WithMessage("Select an option");
            RuleFor(x => x.CityId).NotEmpty().GreaterThan(0).WithMessage("Select your city");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Enter your Job Title");
            RuleFor(x => x.JobDescription).NotEmpty().WithMessage("Enter Job Description");
            RuleFor(x => x.Budget).NotEmpty().GreaterThan(0).WithMessage("Enter your job Budget");
            RuleFor(x => x.JobStartingDate).NotEmpty().WithMessage("Select job Starting Date");
            RuleFor(x => x.JobStartingTime).NotEmpty().WithMessage("Select job Starting Time");
            RuleFor(x => x.Area).NotEmpty().WithMessage("Enter your town name");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Enter your business address");
        }
    }
}
