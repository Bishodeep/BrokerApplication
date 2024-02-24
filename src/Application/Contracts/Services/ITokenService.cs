using System.Security.Claims;

namespace clean.Application.Contracts.Services
{
    public interface ITokenService
    {
        public string GenerateEncodedToken(List<Claim> claims);
    }
}
