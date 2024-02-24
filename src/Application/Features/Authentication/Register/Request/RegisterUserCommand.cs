using clean.Application.Common.Models;
using clean.Application.Common.Models.Authentication;
using MediatR;

namespace clean.Application.Features.Authentication.Register.Request
{
    public class RegisterUserCommand : IRequest<ResponseModel>
    {
        public RegisterUserDto RegisterUserDto { get; set; }
    }
}
