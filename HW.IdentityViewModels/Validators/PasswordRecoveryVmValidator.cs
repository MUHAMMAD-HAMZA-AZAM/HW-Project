using FluentValidation;
using FluentValidation.Results;
using HW.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HW.IdentityViewModels
{

    public class PasswordRecoveryVmValidator : AbstractValidator<PasswordRecoveryVM>
    {
        public PasswordRecoveryVmValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(user => user.EmailOrPhoneNumber).NotEmpty().WithMessage("Enter a valid email or mobile number").Must(BeAnEmailOrPhoneNumber).WithMessage("Enter a valid email or mobile number");
        }

        protected bool BeAnEmailOrPhoneNumber(string emailOrPhoneNumber)
        {
            Match emailMatch = CustomRegularExpressions.Email().Match(emailOrPhoneNumber);
            Match mobileNumberMatch = CustomRegularExpressions.MobileNumber().Match(emailOrPhoneNumber);

            return emailMatch.Success || mobileNumberMatch.Success;
        }
    }
}
