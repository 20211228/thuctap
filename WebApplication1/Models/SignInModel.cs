using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class SignInModel
    {
        
        public string? Email { get; set; }
        
        public string? Password { get; set; }
    }

    public class LoginModelValidator : AbstractValidator<SignInModel>
    {
        public LoginModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(5).WithMessage("Username must be at least 5 characters long.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(7).WithMessage("Password must be at least 8 characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one number.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character (e.g., @, #, $, %, ^, &, *).");
        }
    }
}
    
