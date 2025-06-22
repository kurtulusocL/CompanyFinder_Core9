using CompanyFinder.Entities.Entities;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class PictureValidation:AbstractValidator<Picture>
    {
        public PictureValidation()
        {
            RuleFor(i => i.ImageUrl).NotEmpty().WithMessage("Image can not be null");
        }
    }
}
