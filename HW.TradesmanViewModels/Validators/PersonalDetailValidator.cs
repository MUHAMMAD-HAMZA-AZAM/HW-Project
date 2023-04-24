using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HW.TradesmanViewModels
{
    public class PersonalDetailValidator : AbstractValidator<PersonalDetailVM>
    {
        public PersonalDetailValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.DateOfBirth).NotEmpty().GreaterThanOrEqualTo(new DateTime(1950, 1, 1)).WithMessage("{PropertyName} must be greater than or equal to 01 Jan 1950");
        }
    }
}
