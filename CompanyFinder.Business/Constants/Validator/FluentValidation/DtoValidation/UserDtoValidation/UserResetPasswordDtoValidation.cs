using CompanyFinder.Core.Dtos.UserAuthDtos;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation.DtoValidation.UserDtoValidation
{
    public class UserResetPasswordDtoValidation : AbstractValidator<UserResetPasswordDto>
    {
        public UserResetPasswordDtoValidation()
        {
            RuleFor(i => i.Email).EmailAddress().NotEmpty().WithMessage("Email can not be empty");
            RuleFor(i => i.NewPassword).NotEmpty().WithMessage("New Password can not be empty");
            RuleFor(i => i.ConfirmNewPassword).Equal(i => i.NewPassword).NotEmpty().WithMessage("Confirm New Password can not be empty");
        }
    }
}
