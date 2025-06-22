using CompanyFinder.Entities.Entities;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class AboutValidation : AbstractValidator<About>
    {
        public AboutValidation()
        {
            RuleFor(i => i.Title).NotEmpty().WithMessage("Title can not be null");
            RuleFor(i => i.Subtitle).NotEmpty().WithMessage("Subtitle can not be null");
            RuleFor(i => i.Desc).NotEmpty().WithMessage("Description can not be null");
            RuleFor(i => i.ImageUrl).NotEmpty().WithMessage("Image can not be null");
        }
    }
}
