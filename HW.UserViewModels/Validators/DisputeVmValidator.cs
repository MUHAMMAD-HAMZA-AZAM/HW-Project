using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels.Validators
{
    public class DisputeVmValidator : AbstractValidator<DisputeVM>
    {
        public DisputeVmValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.JobDetailId).NotEmpty().GreaterThan(0).WithMessage("Select an option");
            RuleFor(x => x.CreatedOn).NotEmpty().WithMessage("Select Date");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Enter subject of an issue");
            RuleFor(x => x.Message).NotEmpty().WithMessage("Enter your message");
        }
    }
}
