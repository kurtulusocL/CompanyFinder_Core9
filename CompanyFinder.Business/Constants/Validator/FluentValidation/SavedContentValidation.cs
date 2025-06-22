using CompanyFinder.Entities.Entities;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class SavedContentValidation : AbstractValidator<SavedContent>
    {
        public SavedContentValidation()
        {
            RuleFor(i => i.IsSaved).NotEmpty().WithMessage("IsSaved can not be empty");
            RuleFor(i => i.SavedDate).NotEmpty().WithMessage("Saved Date can not be empty");
            RuleFor(i => i.UserId).NotEmpty().WithMessage("UserId can not be empty");
        }
    }
}
