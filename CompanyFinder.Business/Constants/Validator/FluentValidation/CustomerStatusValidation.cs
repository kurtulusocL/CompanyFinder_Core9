using CompanyFinder.Entities.Entities;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class CustomerStatusValidation : AbstractValidator<CustomerStatus>
    {
        public CustomerStatusValidation()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("Status name can not be empty");
        }
    }
}