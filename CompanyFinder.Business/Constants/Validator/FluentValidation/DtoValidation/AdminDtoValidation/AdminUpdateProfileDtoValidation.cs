using CompanyFinder.Core.Dtos.AdminAuthDtos;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation.DtoValidation.AdminDtoValidation
{
    public class AdminUpdateProfileDtoValidation : AbstractValidator<AdminUpdateProfileDto>
    {
        public AdminUpdateProfileDtoValidation()
        {
            RuleFor(i => i.Username).NotEmpty().WithMessage("Username can not be empty");
            RuleFor(i => i.PhoneNumber).NotEmpty().Must(i => i.All(char.IsDigit)).WithMessage("Phone Number can not be empty");
            RuleFor(i => i.Email).EmailAddress().NotEmpty().WithMessage("Email can not be empty");
            RuleFor(i => i.Country).NotEmpty().WithMessage("Country can not be empty");
            RuleFor(i => i.City).NotEmpty().WithMessage("City can not be empty");
            RuleFor(i => i.UpdatedDate).NotEmpty().WithMessage("Updated Date can not be empty");
        }
    }
}
