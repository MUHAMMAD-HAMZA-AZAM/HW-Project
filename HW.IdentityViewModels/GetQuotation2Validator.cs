using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HW.IdentityViewModels
{
   public class GetQuotation2Validator : AbstractValidator<GetQuotation2VM>
    {
        public GetQuotation2Validator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Bids).NotEmpty().WithMessage("Select option");
            RuleFor(x => x.JobStartDate).NotEmpty().WithMessage("Give Your Job Start Date");
            RuleFor(x => x.JobStartTime).NotEmpty().WithMessage("Give Your Job Start Time");
            RuleFor(x => x.Budget).NotEmpty().WithMessage("Enter your Budget").Must(p => p > 0);
        }
    }
}
