using CompanyFinder.Core.Dtos.AdminAuthDtos;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation.DtoValidation.AdminDtoValidation
{
    public class AdminLoginDtoValidation : AbstractValidator<AdminLoginDto>
    {
        public AdminLoginDtoValidation()
        {
            RuleFor(i => i.Email).EmailAddress().NotEmpty().WithMessage("Email Address can not be empty");
            RuleFor(i => i.Password).NotEmpty().WithMessage("Password can not ve empty");
        }
    }
}
