using clean.Application.Features.Authentication.Register.Request;
using FluentValidation;

namespace clean.Application.Features.Authentication.Register.Validators
{
    public class RegisterUserCommandValidator:AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.RegisterUserDto).NotNull().WithMessage("No incoming data").DependentRules(() =>
            {
                RuleFor(x => x.RegisterUserDto.Email).NotEmpty().WithMessage("Email cannot be empty");
                RuleFor(x => x.RegisterUserDto.Password).NotEmpty().WithMessage("Password cannot be empty");
                RuleFor(x => x.RegisterUserDto.RoleId).NotEmpty().WithMessage("Role cannot be empty");
            });
           
        }
    }
}
