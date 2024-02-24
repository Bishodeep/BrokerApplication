using clean.Application.Features.Authentication.Login.Requests;
using FluentValidation;

namespace clean.Application.Features.Authentication.Login.Validators
{
    public class LoginCommandValidator:AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User Name cannot be null");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be null");
        }
    }
}
