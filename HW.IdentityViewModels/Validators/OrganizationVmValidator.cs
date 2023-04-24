using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HW.IdentityViewModels
{
    public class OrganizationVmValidator : AbstractValidator<TmBusinessDetailVM>
    {
        public OrganizationVmValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.TradesmanId).NotEmpty();
            RuleFor(x => x.CompanyName).NotEmpty();
            //RuleFor(x => x.CompanyRegNo).NotEmpty();
            RuleFor(x => x.SkillIds).NotEmpty().WithMessage("Select at least one service");
            //RuleFor(x => x.City).NotEmpty().WithMessage("Select an option");
            RuleFor(x => x.Town).NotEmpty();
            RuleFor(x => x.BusinessAddress).NotEmpty();
        }
    }
}
