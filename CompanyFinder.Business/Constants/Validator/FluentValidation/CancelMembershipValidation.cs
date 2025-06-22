using CompanyFinder.Entities.Entities;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class CancelMembershipValidation:AbstractValidator<CancelMembership>
    {
        public CancelMembershipValidation()
        {
            RuleFor(i => i.Title).NotNull().WithMessage("Title can not be null");
            RuleFor(i => i.ExpectedCancelDate).GreaterThanOrEqualTo(DateTime.Today).NotNull().WithMessage("Expected Cancel Date can not be null and Date can not be earlier than today.");
            RuleFor(i => i.CancelMembershipReasonId).NotNull().WithMessage("CancelMembershipReasonId can not be null");
            RuleFor(i => i.UserId).NotNull().WithMessage("UserId can not be null");
        }
    }
}
