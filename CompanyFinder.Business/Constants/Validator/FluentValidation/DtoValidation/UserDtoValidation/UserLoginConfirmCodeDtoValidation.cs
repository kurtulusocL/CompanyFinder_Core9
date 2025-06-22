using CompanyFinder.Core.Dtos.UserAuthDtos;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation.DtoValidation.UserDtoValidation
{
    public class UserLoginConfirmCodeDtoValidation:AbstractValidator<UserLoginConfirmCodeDto>
    {
        public UserLoginConfirmCodeDtoValidation()
        {
            RuleFor(i => i.Email).NotEmpty().EmailAddress().WithMessage("Email Address can not be empty");
            RuleFor(i => i.LoginConfirmCode).NotEmpty().WithMessage("Code can not be empty");
        }
    }
}
