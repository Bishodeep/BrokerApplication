using clean.Application.Common.Models.Authentication;
using clean.Application.Features.Authentication.Login.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrokerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthorizationController : ApiControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto loginRequestDto,CancellationToken cancellationToken)
        {
            return await Mediator.Send(new LoginCommand(loginRequestDto.UserName, loginRequestDto.Password),cancellationToken);
        }

    }
}
