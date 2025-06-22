using CompanyFinder.Entities.Entities;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class ToDoValidation : AbstractValidator<ToDo>
    {
        public ToDoValidation()
        {
            RuleFor(i => i.Title).NotEmpty().WithMessage("Title can not be empty");
            RuleFor(i => i.Desc).NotEmpty().WithMessage("Description can not be empty");
            RuleFor(i => i.StartDate).NotEmpty().WithMessage("StartDate can not be empty");
            RuleFor(i => i.EndDate).NotEmpty().WithMessage("EndDate can not be empty");
            RuleFor(i => i.UserId).NotEmpty().WithMessage("UserId can not be empty");
        }
    }
}
