using CompanyFinder.Entities.Entities;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class ProductValidation : AbstractValidator<Product>
    {
        public ProductValidation()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("Name can not be null");
            RuleFor(i => i.Desc).NotEmpty().WithMessage("Description can not be null");
            RuleFor(i => i.IsCommentable).NotEmpty().WithMessage("IsCommentable can not be null");
            RuleFor(i => i.IsQuestionable).NotEmpty().WithMessage("IsQuestionable can not be null");
            RuleFor(i => i.IsAvailable).NotEmpty().WithMessage("IsAvailable can not be null");
            RuleFor(i => i.ImageUrl).NotEmpty().WithMessage("Image can not be null");
            RuleFor(i => i.ProductCategoryId).NotEmpty().WithMessage("CategoryId can not be null");
            RuleFor(i => i.UserId).NotEmpty().WithMessage("UserId can not be null");
        }
    }
}
