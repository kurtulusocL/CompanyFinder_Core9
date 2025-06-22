using CompanyFinder.Entities.Entities;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class NewsValidation : AbstractValidator<News>
    {
        public NewsValidation()
        {
            RuleFor(i => i.Title).NotEmpty().WithMessage("Title can not be empty");
            RuleFor(i => i.Subtitle).NotEmpty().WithMessage("Subtitle can not be empty");
            RuleFor(i => i.Desc).NotEmpty().WithMessage("Description can not be empty");
            RuleFor(i => i.ImageUrl).NotEmpty().WithMessage("Image can not be empty");
        }
    }
}
