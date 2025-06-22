using CompanyFinder.Entities.Entities;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class ReportValidation : AbstractValidator<Report>
    {
        public ReportValidation()
        {
            RuleFor(i => i.Title).NotEmpty().WithMessage("Title can not be empty");
            RuleFor(i => i.Desc).NotEmpty().WithMessage("Description can not be empty");
            RuleFor(i => i.ReportCategoryId).NotEmpty().WithMessage("ReportCategoryId can not be empty");
            RuleFor(i => i.UserId).NotEmpty().WithMessage("UserId can not be empty");
        }
    }
}
