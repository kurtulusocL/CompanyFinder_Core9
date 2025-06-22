using CompanyFinder.Entities.Entities;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class AppointmentAnswerValidation:AbstractValidator<AppointmentAnswer>
    {
        public AppointmentAnswerValidation()
        {
            RuleFor(i => i.Title).NotEmpty().WithMessage("Title can not be null");
            RuleFor(i => i.Desc).NotEmpty().WithMessage("Description can not be null");
            RuleFor(i => i.ReAppointmentDate).GreaterThanOrEqualTo(DateTime.Today).NotEmpty().WithMessage("Appointment Answer Date can not be null");
            RuleFor(i => i.AppointmentId).NotEmpty().WithMessage("AppointmentId can not be null");
        }
    }
}
