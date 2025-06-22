using CompanyFinder.Entities.Entities;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class CustomerValidation:AbstractValidator<Customer>
    {
        public CustomerValidation()
        {
            RuleFor(i => i.NameSurname).NotEmpty().WithMessage("Name Surname can not be empty");
            RuleFor(i => i.Location).NotEmpty().WithMessage("Location can not be empty");
            RuleFor(i => i.EmailAddress).EmailAddress().NotEmpty().WithMessage("Email Address can not be null");
            RuleFor(i => i.CustomerStatusId).NotEmpty().WithMessage("Customer Status can not be empty");
        }
    }
}
