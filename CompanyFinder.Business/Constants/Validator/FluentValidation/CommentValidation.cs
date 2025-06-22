using CompanyFinder.Entities.Entities;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class CommentValidation : AbstractValidator<Comment>
    {
        public CommentValidation()
        {
            RuleFor(i => i.Text).NotEmpty().WithMessage("Text can not be empty");
            RuleFor(i => i.UserId).NotEmpty().WithMessage("UserId can not be empty");
        }
    }
}
