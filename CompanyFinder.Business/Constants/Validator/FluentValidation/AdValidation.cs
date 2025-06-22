using CompanyFinder.Entities.Entities;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class AdValidation:AbstractValidator<Ad>
    {
        public AdValidation()
        {
            RuleFor(i => i.Text).NotEmpty().WithMessage("Text can not be null");
            RuleFor(i => i.ShowDate).NotEmpty().WithMessage("Show date can not be null");
            RuleFor(i => i.ExpiryDate).NotEmpty().WithMessage("Expiry date can not be null");
            RuleFor(i => i.HasTarget).NotEmpty().WithMessage("Target can not be null");
        }
    }
}
