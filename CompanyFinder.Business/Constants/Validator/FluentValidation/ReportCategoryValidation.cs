using CompanyFinder.Entities.Entities;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class ReportCategoryValidation : AbstractValidator<ReportCategory>
    {
        public ReportCategoryValidation()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("Name can not be empty");
        }
    }
}
