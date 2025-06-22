using CompanyFinder.Entities.Entities;
using FluentValidation;

namespace CompanyFinder.Business.Constants.Validator.FluentValidation
{
    public class StockValidation:AbstractValidator<Stock>
    {
        public StockValidation()
        {
            RuleFor(i => i.Code).NotEmpty().WithMessage("Stock code can not be null");
            RuleFor(i => i.Quantity).NotEmpty().WithMessage("Stock quantity can not be null");
        }
    }
}
