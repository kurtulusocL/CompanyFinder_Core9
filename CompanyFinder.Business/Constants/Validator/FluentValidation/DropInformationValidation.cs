using CompanyFinder.Entities.Entities;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class DropInformationValidation:AbstractValidator<DropInformation>
    {
        public DropInformationValidation()
        {
            RuleFor(i => i.Location).NotEmpty().WithMessage("Location can not be empty");
            RuleFor(i => i.CompanyName).NotEmpty().WithMessage("Company Name can not be empty");
            RuleFor(i => i.Email).EmailAddress().NotEmpty().WithMessage("Email Address can not be empty and can be valid.");
        }
    }
}
