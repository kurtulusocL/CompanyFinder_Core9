using CompanyFinder.Entities.Entities;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class CompanyFormAnswerValidation : AbstractValidator<CompanyFormAnswer>
    {
        public CompanyFormAnswerValidation()
        {
            RuleFor(i => i.Title).NotEmpty().WithMessage("Title can not be empty");
            RuleFor(i => i.Desc).NotEmpty().WithMessage("Description can not be empty");
        }
    }
}
