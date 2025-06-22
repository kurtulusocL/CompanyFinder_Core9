using CompanyFinder.Entities.Entities;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class OurServicesValidation : AbstractValidator<OurServices>
    {
        public OurServicesValidation()
        {
            RuleFor(i => i.Title).NotEmpty().WithMessage("Title can not be null");
            RuleFor(i => i.Desc).NotEmpty().WithMessage("Description can not be null");
        }
    }
}
