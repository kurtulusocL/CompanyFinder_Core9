using CompanyFinder.Entities.Entities.AppUser;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class UserRoleValidation : AbstractValidator<UserRole>
    {
        public UserRoleValidation()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("Name can not be empty");
        }
    }
}
