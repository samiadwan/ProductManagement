using FluentValidation;
using ProductManagement.Models;

namespace ProductManagement.Validators
{
    public class OrderItemValidator : AbstractValidator<OrderItem>
    {
        public OrderItemValidator()
        {
            RuleFor(oi => oi.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be at least 1.");

            RuleFor(oi => oi.ProductId)
                .GreaterThan(0)
                .WithMessage("Product ID must be valid.");

        }
    }
}
