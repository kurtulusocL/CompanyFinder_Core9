using CompanyFinder.Core.Dtos.AdminAuthDtos;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation.DtoValidation.AdminDtoValidation
{
    public class AdminRegisterDtoValidation : AbstractValidator<AdminRegisterDto>
    {
        public AdminRegisterDtoValidation()
        {
            RuleFor(i => i.NameSurname).NotEmpty().WithMessage("Name Surname can not be empty");
            RuleFor(i => i.Username).NotEmpty().WithMessage("Username can not be empty");
            RuleFor(i => i.Country).NotEmpty().WithMessage("Country can not be empty");
            RuleFor(i => i.City).NotEmpty().WithMessage("City can not be empty");
            RuleFor(i => i.Gender).NotEmpty().WithMessage("Gender can not be empty");
            RuleFor(i => i.Title).NotEmpty().WithMessage("Title can not be empty");
            RuleFor(i => i.Birthdate).LessThan(DateTime.Now).NotEmpty().WithMessage("Birthdate can not be empty");
            RuleFor(i => i.Email).EmailAddress().NotEmpty().WithMessage("Email can not be empty");
            RuleFor(i => i.PhoneNumber).NotEmpty().Must(i => i.All(char.IsDigit)).WithMessage("City can not be empty");
            RuleFor(i => i.Password).NotEmpty().WithMessage("Password can not be empty");
            RuleFor(i => i.ConfirmPassword).Equal(i => i.Password).NotEmpty().WithMessage("ConfirmPassword can not be empty");
        }
    }
}
