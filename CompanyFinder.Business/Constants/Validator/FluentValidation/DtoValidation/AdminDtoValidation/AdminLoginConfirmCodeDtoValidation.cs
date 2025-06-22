using CompanyFinder.Core.Dtos.AdminAuthDtos;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation.DtoValidation.AdminDtoValidation
{
    public class AdminLoginConfirmCodeDtoValidation : AbstractValidator<AdminLoginConfirmCodeDto>
    {
        public AdminLoginConfirmCodeDtoValidation()
        {
            RuleFor(i => i.Email).NotEmpty().EmailAddress().WithMessage("Email Address can not be empty");
            RuleFor(i => i.LoginConfirmCode).NotEmpty().WithMessage("Code can not be empty");
        }
    }
}
