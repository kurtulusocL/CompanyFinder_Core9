using CompanyFinder.Entities.Entities.AppUser;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class UserValidation : AbstractValidator<User>
    {
        public UserValidation()
        {
            RuleFor(i => i.NameSurname).NotEmpty().WithMessage("Name Surname can not be empty");
            RuleFor(i => i.Birthdate).LessThan(DateTime.Now).NotEmpty().WithMessage("Birtdate can not be empty");
            RuleFor(i => i.City).NotEmpty().WithMessage("City can not be empty");
            RuleFor(i => i.Country).NotEmpty().WithMessage("Country can not be empty");
            RuleFor(i => i.IsAcceptedPolicies).NotEmpty().WithMessage("IsAcceptedPolicies can not be empty");
            RuleFor(i => i.Email).EmailAddress().NotEmpty().WithMessage("Email Address can not be empty");
            RuleFor(i => i.UserName).NotEmpty().WithMessage("Username can not be empty");
        }
    }
}
