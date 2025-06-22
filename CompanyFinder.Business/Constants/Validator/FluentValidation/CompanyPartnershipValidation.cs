using CompanyFinder.Entities.Entities;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class CompanyPartnershipValidation : AbstractValidator<CompanyPartnership>
    {
        public CompanyPartnershipValidation()
        {
            RuleFor(i => i.Title).NotEmpty().WithMessage("Title can not be empty");
            RuleFor(i => i.Desc).NotEmpty().WithMessage("Description can not be empty");
            RuleFor(i => i.StartDate).NotEmpty().WithMessage("Start Date can not be empty");
            RuleFor(i => i.ExpiryDate).NotEmpty().WithMessage("Expiry Date can not be empty");
            RuleFor(i => i.IsAvailable).NotEmpty().WithMessage("Available Status can not be empty");
        }
    }
}
