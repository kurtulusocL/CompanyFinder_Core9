using CompanyFinder.Entities.Entities;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class SocialMediaValidation:AbstractValidator<SocialMedia>
    {
        public SocialMediaValidation()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("Name can not be empty");
            RuleFor(i => i.Url).NotEmpty().WithMessage("Website Url can not be empty");
            RuleFor(i => i.ImageUrl).NotEmpty().WithMessage("Image can not be empty");
        }
    }
}
