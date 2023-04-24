using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace HW.IdentityViewModels
{

    public class UserRoleVmValidator : AbstractValidator<UserRoleVM>
    {
        public UserRoleVmValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(user => user.UserId).NotEmpty();
            RuleFor(user => user.Role).NotEmpty();
        }
    }
}
