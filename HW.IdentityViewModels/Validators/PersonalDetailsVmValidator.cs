using FluentValidation;
using HW.Utility;
using System;

namespace HW.IdentityViewModels
{
    public class PersonalDetailsVmValidator : AbstractValidator<PersonalDetailsVM>
    {
        public PersonalDetailsVmValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.FirstName).NotEmpty().MinimumLength(2).MaximumLength(50).Matches(CustomRegularExpressions.AlphabetsWithSpaces());
            RuleFor(x => x.LastName).NotEmpty().MinimumLength(2).MaximumLength(50).Matches(CustomRegularExpressions.AlphabetsWithSpaces());
            //RuleFor(x => x.Cnic).NotEmpty().Matches(CustomRegularExpressions.Cnic()).WithMessage("Enter a valid 13-digit CNIC number without hyphens(-).\nE.g: 1234512345671");
            //RuleFor(x => x.Gender).NotEmpty().WithMessage("Select an option").Must(g => g == 1 || g == 2 || g == 3);
            //RuleFor(x => x.DateOfBirth).NotEmpty().GreaterThanOrEqualTo(new DateTime(1950, 1, 1)).WithMessage("{PropertyName} must be greater than or equal to 01 Jan 1950");
        }
    }
}
