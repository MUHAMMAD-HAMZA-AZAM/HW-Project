using FluentValidation;
using HW.Utility;

namespace HW.IdentityViewModels
{

    public class UserVmValidator : AbstractValidator<UserRegisterVM>
    {
        public UserVmValidator(bool isAppleLogin = false, bool fromPersonalDetails = false)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            //RuleFor(user => user.Email).Matches(CustomRegularExpressions.Email()).WithMessage("Enter a valid email address");
            if(!fromPersonalDetails) RuleFor(user => user.Password).NotEmpty().Must(PasswordValidator.BeValid).WithMessage(PasswordValidator.ErrorMsg);
            //RuleFor(user => user.ConfirmPassword).Equal(user => user.Password).WithMessage("Passwords didn't match.");

            //if (!isAppleLogin)
            //{
            //    RuleFor(user => user.PhoneNumber).NotEmpty().Matches(CustomRegularExpressions.MobileNumber()).WithMessage("Enter a valid 11-digit mobile number without hyphens(-).\nE.g: 03001234567");
            //}

            RuleFor(x => x.Role).NotEmpty().WithMessage("Select an option");

        }
    }
}
