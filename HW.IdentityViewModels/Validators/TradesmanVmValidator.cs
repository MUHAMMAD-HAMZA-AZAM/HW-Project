using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HW.IdentityViewModels
{
    public class TradesmanVmValidator : AbstractValidator<TmBusinessDetailVM>
    {
        public TradesmanVmValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.TradesmanId).NotEmpty();
            //RuleFor(x => x.SkillIds).NotEmpty().WithMessage("Select an option").Must(list => list.Count <= 1).WithMessage("Tradesman must have only 1 skill");
           // RuleFor(x => x.SkillIds).NotEmpty().WithMessage("Select at least one service");
            RuleFor(x => x.TravelingDistance).NotEmpty().WithMessage("Select an option").Must(d => d > 0);
            //RuleFor(x => x.CityId).NotEmpty().WithMessage("Select an option");
            RuleFor(x => x.Town).NotEmpty();
           // RuleFor(x => x.BusinessAddress).NotEmpty();
        }
    }
}
