using clean.Application.Features.Property.Requests;
using FluentValidation;

namespace clean.Application.Features.Property.Validator
{
    public class CreatePropertyValidator:AbstractValidator<CreatePropertyCommand>
    {
        public CreatePropertyValidator()
        {
            RuleFor(x => x.PropertyCreateDto).NotEmpty().WithMessage("No incoming data.").DependentRules(() =>
            {
                RuleFor(x => x.PropertyCreateDto.OwnerName).NotEmpty().WithMessage("Owner name cannot be empty");
                RuleFor(x => x.PropertyCreateDto.Location).NotEmpty().WithMessage("Location cannot be empty");
                RuleFor(x => x.PropertyCreateDto.OwnerContact).NotEmpty().WithMessage("Owner Contact cannot be empty");
            });
        }
    }
}
