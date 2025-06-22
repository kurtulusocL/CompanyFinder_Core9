using CompanyFinder.Entities.Entities;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class QuestionAnswerValidation : AbstractValidator<QuestionAnswer>
    {
        public QuestionAnswerValidation()
        {
            RuleFor(i => i.Answer).NotEmpty().WithMessage("Answer can not be empty");
            RuleFor(i => i.UserId).NotEmpty().WithMessage("UserId can not be null");
        }
    }
}
