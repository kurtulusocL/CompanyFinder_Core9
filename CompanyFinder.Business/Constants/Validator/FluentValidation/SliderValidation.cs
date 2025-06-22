using CompanyFinder.Entities.Entities;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class SliderValidation : AbstractValidator<Slider>
    {
        public SliderValidation()
        {
            RuleFor(i => i.ImageUrl).NotEmpty().WithMessage("Image can not be empty");
        }
    }
}
