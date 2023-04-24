using FluentValidation;
using HW.Utility;

namespace HW.IdentityViewModels.Validators
{
    public class PersonalDetailValidator : AbstractValidator<HW.SupplierViewModels.PersonalDetailsVM>
    {
        public PersonalDetailValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.EmailAddress).NotEmpty().Matches(CustomRegularExpressions.Email()).WithMessage("Enter a valid email address");
            RuleFor(x => x.MobileNumber).NotEmpty().Matches(CustomRegularExpressions.MobileNumber()).WithMessage("Enter a valid 11-digit mobile number without hyphens(-).\nE.g: 03001234567");
        }
    }
}
