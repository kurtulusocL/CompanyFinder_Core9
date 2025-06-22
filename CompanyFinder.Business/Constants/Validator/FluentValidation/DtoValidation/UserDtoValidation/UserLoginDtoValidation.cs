using CompanyFinder.Core.Dtos.UserAuthDtos;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation.DtoValidation.UserDtoValidation
{
    public class UserLoginDtoValidation:AbstractValidator<UserLoginDto>
    {
        public UserLoginDtoValidation()
        {
            RuleFor(i => i.Email).EmailAddress().NotEmpty().WithMessage("Email Address can not be empty");
            RuleFor(i => i.Password).NotEmpty().WithMessage("Password can not ve empty");
        }
    }
}
