using CompanyFinder.Entities.Entities;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class CompanyValidation : AbstractValidator<Company>
    {
        public CompanyValidation()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("Name can not be empty");
            RuleFor(i => i.Desc).NotEmpty().WithMessage("Description can not be empty");
            RuleFor(i => i.FoundationDate).LessThanOrEqualTo(DateTime.Now).NotEmpty().WithMessage("Foundation Date can not be empty and also foundation date can not be after today.");
            RuleFor(i => i.IsCommentable).NotEmpty().WithMessage("IsCommentable can not be empty");
            RuleFor(i => i.CompanyCategoryId).NotEmpty().WithMessage("Company CateogryId can not be empty");
            RuleFor(i => i.CountryId).NotEmpty().WithMessage("CountryId can not be empty");          
            RuleFor(i => i.UserId).NotEmpty().WithMessage("UserId can not be empty");            
        }
    }
}
