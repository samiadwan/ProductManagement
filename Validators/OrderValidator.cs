using FluentValidation;
using ProductManagement.Models;

namespace ProductManagement.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(o => o.OrderDate)
                 .NotEmpty()
                 .WithMessage("Order date is required.");

            RuleFor(o => o.UserId)
                .GreaterThan(0)
                .WithMessage("User ID must be valid.");

            RuleForEach(o => o.OrderItems)
                .SetValidator(new OrderItemValidator());
        }
    }
}
