using clean.Application.Common.Models;
using clean.Application.Contracts.Services;
using clean.Application.Features.Authentication.Register.Request;
using MediatR;

namespace clean.Application.Features.Authentication.Register.Command
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ResponseModel>
    {
        private readonly IIdentityService _identityService;

        public RegisterUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<ResponseModel> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.CreateUserAsync(request.RegisterUserDto, cancellationToken);
            if(result.Result.Status)
                return new ResponseModel { Status= result.Result.Status ,Message="User Registered successfully", StatusCode = 200 };
            else
                return new ResponseModel { Status = result.Result.Status, Message = result.Result.Errors.ToString() };
        }
    }
}
