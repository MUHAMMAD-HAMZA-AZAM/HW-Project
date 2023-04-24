using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HW.TradesmanViewModels
{
    public class EditBidValidator : AbstractValidator<EditBidVM>
    {
        public EditBidValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Amount).NotEmpty().GreaterThan(0);
        }
    }
}
