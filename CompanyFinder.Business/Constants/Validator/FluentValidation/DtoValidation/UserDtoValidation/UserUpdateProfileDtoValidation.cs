using CompanyFinder.Core.Dtos.UserAuthDtos;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation.DtoValidation.UserDtoValidation
{
    public class UserUpdateProfileDtoValidation : AbstractValidator<UserUpdateProfileDto>
    {
        public UserUpdateProfileDtoValidation()
        {
            RuleFor(i => i.Email).EmailAddress().NotEmpty().WithMessage("Email Address can not be empty");
            RuleFor(i => i.PhoneNumber).NotEmpty().Must(i => i.All(char.IsDigit)).WithMessage("Phone Number can not be empty");
            RuleFor(i => i.Country).NotEmpty().WithMessage("Country can not be empty");
            RuleFor(i => i.City).NotEmpty().WithMessage("City can not be empty");
            RuleFor(i => i.UpdatedDate).NotEmpty().WithMessage("Updated Date can not be empty");
        }
    }
}
