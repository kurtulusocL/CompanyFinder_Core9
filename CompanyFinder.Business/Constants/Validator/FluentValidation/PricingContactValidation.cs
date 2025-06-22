using CompanyFinder.Entities.Entities;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class PricingContactValidation:AbstractValidator<PricingContact>
    {
        public PricingContactValidation()
        {
            RuleFor(i => i.NameSurname).NotEmpty().WithMessage("Name Surname can not be empty");
            RuleFor(i => i.CompanyName).NotEmpty().WithMessage("Company Name can not be empty");
            RuleFor(i => i.PhoneNumber).Must(i => i.All(char.IsDigit)).NotEmpty().WithMessage("Phone Number can not be empty");
            RuleFor(i => i.Desc).NotEmpty().WithMessage("Contact message can not be empty");
        }
    }
}
