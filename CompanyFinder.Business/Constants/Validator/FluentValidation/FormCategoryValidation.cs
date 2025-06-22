using CompanyFinder.Entities.Entities;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class FormCategoryValidation : AbstractValidator<FormCategory>
    {
        public FormCategoryValidation()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("Name can not be empty");
        }
    }
}
