using FluentValidation;
using HW.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace HW.IdentityViewModels
{
    public class PdUpdateVmValidator : AbstractValidator<PersonalDetailsVM>
    {
        public PdUpdateVmValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            //RuleFor(user => user.MobileNumber).NotEmpty().Matches(CustomRegularExpressions.MobileNumber()).WithMessage("Enter a valid 11-digit mobile number without hyphens(-).\nE.g: 03001234567");
        }
    }
}
