using CompanyFinder.Core.Dtos.UserAuthDtos;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation.DtoValidation.UserDtoValidation
{
    public class UserConfirmCodeDtoValidation : AbstractValidator<UserConfirmCodeDto>
    {
        public UserConfirmCodeDtoValidation()
        {
            RuleFor(i => i.Email).NotEmpty().EmailAddress().WithMessage("Email Address can not be null");
            RuleFor(i => i.ConfirmCode).NotEmpty().WithMessage("Confirm Code can not be null and must be 6 characters");
        }
    }
}
