using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace HW.IdentityViewModels
{

    public class ResetPasswordVmValidator : AbstractValidator<ResetPasswordVM>
    {
        public ResetPasswordVmValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(user => user.UserId).NotEmpty();
            RuleFor(user => user.PasswordResetToken).NotEmpty();
            RuleFor(user => user.Password).NotEmpty().Must(PasswordValidator.BeValid).WithMessage(PasswordValidator.ErrorMsg);
            RuleFor(user => user.ConfirmPassword).Equal(user => user.Password).WithMessage("Passwords didn't match.");
        }
    }
}
