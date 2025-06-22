using CompanyFinder.Entities.Entities;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class CompanyCategoryValidation : AbstractValidator<CompanyCategory>
    {
        public CompanyCategoryValidation()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("Name can not be null");
        }
    }
}
