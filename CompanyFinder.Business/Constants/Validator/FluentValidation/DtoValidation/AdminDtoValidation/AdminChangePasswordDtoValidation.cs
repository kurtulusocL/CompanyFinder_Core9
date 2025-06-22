using CompanyFinder.Core.Dtos.AdminAuthDtos;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation.DtoValidation.AdminDtoValidation
{
    public class AdminChangePasswordDtoValidation : AbstractValidator<AdminChangePasswordDto>
    {
        public AdminChangePasswordDtoValidation()
        {
            RuleFor(i => i.CurrentPassword).NotEmpty().WithMessage("Current Password can not be empty");
            RuleFor(i => i.NewPassword).NotEmpty().WithMessage("New Password can not be empty");
            RuleFor(i => i.ConfirmNewPassword).Equal(i => i.NewPassword).NotEmpty().WithMessage("Confirm New Password can not be empty");
        }
    }
}
