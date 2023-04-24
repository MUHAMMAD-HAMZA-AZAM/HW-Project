using FluentValidation;
using HW.UserWebViewModels;
using HW.Utility;
using System;
using System.Collections.Generic;
using System.Text;
namespace HW.IdentityViewModels.Validators
{
    public class WebResgistrationValidaor : AbstractValidator<WebRegistrationBasicVM>
    {
        public WebResgistrationValidaor()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.FirstName).NotEmpty().MinimumLength(2).MaximumLength(50).Matches(CustomRegularExpressions.AlphabetsOnly());
            RuleFor(x => x.LastName).NotEmpty().MinimumLength(2).MaximumLength(50).Matches(CustomRegularExpressions.AlphabetsOnly());
            RuleFor(x => x.Gender).NotEmpty().NotEqual("Gender").WithMessage("Select an option");
            RuleFor(user => user.EmailAddress).NotEmpty().Matches(CustomRegularExpressions.Email()).WithMessage("Enter a valid email address");
            RuleFor(user => user.Password).NotEmpty().Must(PasswordValidator.BeValid).WithMessage(PasswordValidator.ErrorMsg);
            RuleFor(user => user.PhoneNumber).NotEmpty().Matches(CustomRegularExpressions.MobileNumber()).WithMessage("Enter a valid 11-digit mobile number without hyphens(-).\nE.g: 03001234567");
            RuleFor(x => x.City).NotEmpty().NotEqual("City").WithMessage("Select an option");
        }
    }
}
