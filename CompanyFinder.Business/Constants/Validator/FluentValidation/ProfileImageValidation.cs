using CompanyFinder.Entities.Entities;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class ProfileImageValidation : AbstractValidator<ProfileImage>
    {
        public ProfileImageValidation()
        {
            RuleFor(i => i.ImageUrl).NotEmpty().WithMessage("Image can not be null");
            RuleFor(i => i.UserId).NotEmpty().WithMessage("UserId can not be null");
        }
    }
}
