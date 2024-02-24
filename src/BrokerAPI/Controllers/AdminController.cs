using clean.Application.Common.Models;
using clean.Application.Common.Models.Authentication;
using clean.Application.Features.Authentication.Register.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrokerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdminController : ApiControllerBase
    {
        [HttpPost("Register")]
        public async Task<ActionResult<ResponseModel>> Register(RegisterUserDto registerUserDto, CancellationToken cancellationToken)
        {
            return await Mediator.Send(new RegisterUserCommand { RegisterUserDto = registerUserDto }, cancellationToken);
        }
        [HttpGet("Roles")]
        public async Task<ActionResult<List<RolesDto>>> GetRoles()
        {
            return await Mediator.Send(new GetRolesQuery());
        }
    }
}
