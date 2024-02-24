using clean.Application.Common.Models.Authentication;
using clean.Application.Common.Security;
using MediatR;

namespace clean.Application.Features.Authentication.Register.Request
{
    [Authorize(Roles = "Administrator")]
    public class GetRolesQuery:IRequest<List<RolesDto>>
    {
    }
}
