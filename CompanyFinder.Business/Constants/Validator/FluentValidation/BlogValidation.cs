using CompanyFinder.Entities.Entities;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class BlogValidation : AbstractValidator<Blog>
    {
        public BlogValidation()
        {
            RuleFor(i => i.Title).NotNull().WithMessage("Title can not be null");
            RuleFor(i => i.Desc).NotNull().WithMessage("Description can not be null");
            RuleFor(i => i.ImageUrl).NotNull().WithMessage("ImageUrl can not be null");
            RuleFor(i => i.BlogCategoryId).NotNull().WithMessage("BlogCategoryId can not be null");            
            RuleFor(i => i.UserId).NotNull().WithMessage("UserId can not be null");
        }
    }
}
