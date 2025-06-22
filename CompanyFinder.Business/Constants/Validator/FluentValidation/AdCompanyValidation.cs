using CompanyFinder.Entities.Entities;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class AdCompanyValidation : AbstractValidator<AdCompany>
    {
        public AdCompanyValidation()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("Name can not be empty");
            RuleFor(i => i.ContactPerson).NotEmpty().WithMessage("Contact Person can not be empty");
            RuleFor(i => i.Email).EmailAddress().NotEmpty().WithMessage("Email can not be empty");
            RuleFor(i => i.PhoneNumber).NotEmpty().Must(i => i.All(char.IsDigit)).WithMessage("Phone Number can not be empty and must be number");
            RuleFor(i => i.City).NotEmpty().WithMessage("City can not be empty");
            RuleFor(i => i.Country).NotEmpty().WithMessage("Country can not be empty");
        }
    }
}
