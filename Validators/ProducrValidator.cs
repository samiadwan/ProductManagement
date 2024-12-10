using FluentValidation;
using ProductManagement.Models;

namespace ProductManagement.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("Product name is required.");

            RuleFor(p => p.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price must be a positive number.");
        }
    }
}
