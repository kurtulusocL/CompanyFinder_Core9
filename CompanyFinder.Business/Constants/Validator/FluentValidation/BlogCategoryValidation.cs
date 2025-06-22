using CompanyFinder.Entities.Entities;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class BlogCategoryValidation:AbstractValidator<BlogCategory>
    {
        public BlogCategoryValidation()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("Name can nut be null");
        }
    }
}
