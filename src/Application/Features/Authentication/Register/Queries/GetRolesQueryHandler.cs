using clean.Application.Common.Models.Authentication;
using clean.Application.Contracts.Services;
using clean.Application.Features.Authentication.Register.Request;
using MediatR;

namespace clean.Application.Features.Authentication.Register.Queries
{
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, List<RolesDto>>
    {
        private readonly IIdentityService _identityService;

        public GetRolesQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<List<RolesDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            return await _identityService.GetAllRolesAsync();
        }
    }
}
