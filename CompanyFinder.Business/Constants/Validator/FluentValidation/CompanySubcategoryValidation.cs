using CompanyFinder.Entities.Entities;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class CompanySubcategoryValidation:AbstractValidator<CompanySubcategory>
    {
        public CompanySubcategoryValidation()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("Name can nut be null");
        }
    }
}
