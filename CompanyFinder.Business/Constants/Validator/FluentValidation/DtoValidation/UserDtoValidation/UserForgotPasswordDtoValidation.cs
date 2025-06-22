using CompanyFinder.Core.Dtos.UserAuthDtos;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation.DtoValidation.UserDtoValidation
{
    public class UserForgotPasswordDtoValidation : AbstractValidator<UserForgotPasswordDto>
    {
        public UserForgotPasswordDtoValidation()
        {
            RuleFor(i => i.Email).EmailAddress().NotEmpty().WithMessage("Email Address can not be empty");
        }
    }
}
