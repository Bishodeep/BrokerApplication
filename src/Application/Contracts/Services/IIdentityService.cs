using clean.Application.Common.Models;
using clean.Application.Common.Models.Authentication;

namespace clean.Application.Contracts.Services;

public interface IIdentityService
{
    Task<bool> IsInRoleAsync(string userId, string role);

    Task<LoginResponseDto> AuthenticateAsync(string userName, string password);

    Task<(Result Result, string UserId)> CreateUserAsync(RegisterUserDto registerUserDto, CancellationToken cancellationToken);
    Task<List<RolesDto>> GetAllRolesAsync();
}
