using clean.Application.Common.Models.Authentication;
using clean.Application.Contracts.Services;
using clean.Application.Features.Authentication.Login.Requests;
using MediatR;

namespace clean.Application.Features.Authentication.Login.Command
{

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto>
    {
        private readonly IIdentityService _identityService;

        public LoginCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.AuthenticateAsync(request.UserName,request.Password);
        }
    }
}
