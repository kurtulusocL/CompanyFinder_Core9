using CompanyFinder.Entities.Entities;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class AnnouncementValidation : AbstractValidator<Announcement>
    {
        public AnnouncementValidation()
        {
            RuleFor(i => i.Title).NotEmpty().WithMessage("Name can not be empty");
            RuleFor(i => i.Desc).NotEmpty().WithMessage("Description can not be empty");
            RuleFor(i => i.ForAdmin).NotEmpty().WithMessage("For Admin can not be empty");
            RuleFor(i => i.ForCompany).NotEmpty().WithMessage("For Company can not be empty");
            RuleFor(i => i.ForEverybody).NotEmpty().WithMessage("For Everybody can not be empty");
        }
    }
}
