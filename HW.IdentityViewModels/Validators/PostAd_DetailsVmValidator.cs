using FluentValidation;
using HW.SupplierViewModels;

namespace HW.IdentityViewModels.Validators
{
    public class PostAd_DetailsVmValidator : AbstractValidator<PostAdValidators>
    {
        public PostAd_DetailsVmValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Category).NotEmpty().WithMessage("Select category").Must(p => p > 0);
            RuleFor(x => x.SubCategory).NotEmpty().WithMessage("Select subcatgory").Must(p => p > 0);
            RuleFor(x => x.JobTitle).NotEmpty().WithMessage("Enter ad title.");
            RuleFor(x => x.JobDescription).NotEmpty().WithMessage("Enter your ad description");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Enter product price").Must(p => p > 0);
            RuleFor(x => x.Town).NotEmpty().WithMessage("Enter town");
            RuleFor(x => x.City).NotEmpty().WithMessage("Select city").Must(p => p > 0);
            RuleFor(x => x.Address).NotEmpty().WithMessage("Enter business address");
        }
    }
}