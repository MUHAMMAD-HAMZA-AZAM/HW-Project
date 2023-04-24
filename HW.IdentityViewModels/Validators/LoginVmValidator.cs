using FluentValidation;
using FluentValidation.Results;
using HW.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HW.IdentityViewModels
{

    public class LoginVmValidator : AbstractValidator<LoginVM>
    {
        public LoginVmValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(user => user.EmailOrPhoneNumber).NotEmpty().WithMessage("Enter a valid email or phone number").Must(BeAnEmailOrPhoneNumber).WithMessage("Enter a valid email or phone number");
            RuleFor(user => user.Password).NotEmpty();
        }

        protected bool BeAnEmailOrPhoneNumber(string emailOrPhoneNumber)
        {
            Match emailMatch = CustomRegularExpressions.Email().Match(emailOrPhoneNumber);
            Match mobileNumberMatch = CustomRegularExpressions.MobileNumber().Match(emailOrPhoneNumber);

            return emailMatch.Success || mobileNumberMatch.Success;
        }
    }
}
