using CompanyFinder.Entities.Entities;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class ContactValidation:AbstractValidator<Contact>
    {
        public ContactValidation()
        {
            RuleFor(i => i.City).NotEmpty().WithMessage("City can not be null");
            RuleFor(i => i.Country).NotEmpty().WithMessage("Country can not be null");
            RuleFor(i => i.PrincipalEmail).EmailAddress().NotEmpty().WithMessage("Principal Email can not be null");
            RuleFor(i => i.BusinessEmail).EmailAddress().NotEmpty().WithMessage("Business Email can not be null");
        }
    }
}
