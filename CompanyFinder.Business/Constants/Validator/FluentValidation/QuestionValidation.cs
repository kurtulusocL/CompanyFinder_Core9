using CompanyFinder.Entities.Entities;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class QuestionValidation:AbstractValidator<Question>
    {
        public QuestionValidation()
        {
            RuleFor(i => i.Text).NotEmpty().WithMessage("Text can not be null");
            RuleFor(i => i.UserId).NotEmpty().WithMessage("UserId can not be null");
        }
    }
}
