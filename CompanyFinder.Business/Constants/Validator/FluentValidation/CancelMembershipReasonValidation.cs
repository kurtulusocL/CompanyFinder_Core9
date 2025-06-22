using CompanyFinder.Entities.Entities;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class CancelMembershipReasonValidation:AbstractValidator<CancelMembershipReason>
    {
        public CancelMembershipReasonValidation()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("Name can not be null");
        }
    }
}
