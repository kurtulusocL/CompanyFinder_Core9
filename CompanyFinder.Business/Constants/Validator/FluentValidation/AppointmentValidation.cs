using CompanyFinder.Entities.Entities;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class AppointmentValidation : AbstractValidator<Appointment>
    {
        public AppointmentValidation()
        {
            RuleFor(i => i.Subject).NotEmpty().WithMessage("Subject can not be null");
            RuleFor(i => i.Desc).NotEmpty().WithMessage("Description can not be null");
            RuleFor(i => i.AppointmentDate).GreaterThanOrEqualTo(DateTime.Today).NotEmpty().WithMessage("Appointment Date can not be null");
            RuleFor(i => i.UserId).NotEmpty().WithMessage("UserId can not be null");
        }
    }
}
