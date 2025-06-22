using CompanyFinder.Entities.Entities;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class CompanyMessageValidation:AbstractValidator<CompanyMessage>
    {
        public CompanyMessageValidation()
        {
            RuleFor(i => i.Subject).NotEmpty().WithMessage("Subject can not be null");
            RuleFor(i => i.Desc).NotEmpty().WithMessage("Description can not be null");
            RuleFor(i => i.CompanyId).NotEmpty().WithMessage("CompanyId can not be null");
            RuleFor(i => i.UserId).NotEmpty().WithMessage("UserId can not be null");
        }
    }
}
