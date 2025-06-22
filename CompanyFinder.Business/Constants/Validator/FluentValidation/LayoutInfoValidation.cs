using CompanyFinder.Entities.Entities;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class LayoutInfoValidation : AbstractValidator<LayoutInfo>
    {
        public LayoutInfoValidation()
        {
            RuleFor(i => i.Title).NotEmpty().WithMessage("Title can not be null");
            RuleFor(i => i.Author).NotEmpty().WithMessage("Author can not be null");
            RuleFor(i => i.Keyword).NotEmpty().WithMessage("Keyword can not be null");
            RuleFor(i => i.Content).NotEmpty().WithMessage("Content can not be null");
            RuleFor(i => i.Language).NotEmpty().WithMessage("Language can not be null");
            RuleFor(i => i.Icon).NotEmpty().WithMessage("Icon can not be null");
        }
    }
}
