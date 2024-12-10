using FluentValidation;
using ProductManagement.Models;

namespace ProductManagement.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.Name)
                .NotEmpty()
                .WithMessage("Name is required.");

            RuleFor(u => u.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email format.");

            //RuleFor(user => user.Address)
            //    .NotNull().WithMessage("Address details are required.");

        }
    }
}
