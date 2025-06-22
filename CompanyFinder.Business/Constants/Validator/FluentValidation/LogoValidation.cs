using CompanyFinder.Entities.Entities;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class LogoValidation:AbstractValidator<Logo>
    {
        public LogoValidation()
        {
            RuleFor(i => i.ImageUrl).NotEmpty().WithMessage("Image can not be null");
        }
    }
}
