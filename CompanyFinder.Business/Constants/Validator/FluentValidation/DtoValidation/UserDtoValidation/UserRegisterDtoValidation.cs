using CompanyFinder.Core.Dtos.UserAuthDtos;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation.DtoValidation.UserDtoValidation
{
    public class UserRegisterDtoValidation : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterDtoValidation()
        {
            RuleFor(i => i.NameSurname).NotEmpty().WithMessage("Name Surname can not be empty");            
            RuleFor(i => i.Country).NotEmpty().WithMessage("Country can not be empty");
            RuleFor(i => i.City).NotEmpty().WithMessage("City can not be empty");
            RuleFor(i => i.Birthdate).LessThan(DateTime.Now).NotEmpty().WithMessage("Birthdate can not be empty");
            RuleFor(i => i.PhoneNumber).NotEmpty().Must(i => i.All(char.IsDigit)).WithMessage("Phone Number can not be empty");
            RuleFor(i => i.Email).EmailAddress().NotEmpty().WithMessage("Email can not be empty");
            RuleFor(i => i.Password).NotEmpty().WithMessage("Password can not be empty");
            RuleFor(i => i.ConfirmPassword).Equal(i=>i.Password).NotEmpty().WithMessage("ConfirmPassword can not be empty");
            RuleFor(i => i.IsAcceptedPolicies).NotEmpty().WithMessage("IsAccepted Policies can not be empty");
        }
    }
}
