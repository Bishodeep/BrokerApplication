using Application.Common.Exceptions;
using clean.Application.Common.Constants;
using clean.Application.Common.Models;
using clean.Application.Common.Models.Authentication;
using clean.Application.Contracts.Services;
using clean.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Data;
using System.Security.Claims;

namespace clean.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    RoleManager<ApplicationRole> _roleManager;
    private readonly ITokenService _tokenService;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<ApplicationRole> roleManager,
        ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _roleManager = roleManager;
    }
    public async Task<(Result Result, string UserId)> CreateUserAsync(RegisterUserDto registerUserDto, CancellationToken cancellation)
    {
        var user = new ApplicationUser
        {
            UserName = registerUserDto.Email,
            Email = registerUserDto.Email,
            FirstName = registerUserDto.FirstName,
            LastName = registerUserDto.LastName,
            PhoneNumber = registerUserDto.PhoneNumber,
            StreetAddress = registerUserDto.StreetAddress,
            City = registerUserDto.City,
        };
        var userRole = _roleManager.Roles.FirstOrDefault(x => x.Id == registerUserDto.RoleId);
        var result = await _userManager.CreateAsync(user, registerUserDto.Password);
        await _userManager.AddToRoleAsync(user, userRole.Name);
        return (result.ToApplicationResult(), user.Id);
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<LoginResponseDto> AuthenticateAsync(string userName, string password)
    {
        var user = _userManager.Users.Where(u => u.UserName == userName).FirstOrDefault();
        if (user != null)
        {
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (result.Succeeded)
            {
                var claims = await GenerateClaimsIdentity(user);
                var token = _tokenService.GenerateEncodedToken(claims);
                return new LoginResponseDto() { Token = token, UserName = user.UserName };
            }
        }
        throw new UnAuthorizedException();
    }
    #region Helpers
    private async Task<List<Claim>> GenerateClaimsIdentity(ApplicationUser user)
    {
        var claims = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimType.UserName, user.UserName),
                new Claim(ClaimType.UserId, user.Id)
            };

        var roles = await _userManager.GetRolesAsync(user);

        claims.AddRange(roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));

        return claims;
    }

    public async Task<List<RolesDto>> GetAllRolesAsync()
    {
        var roles= await _roleManager.Roles.ToListAsync();
        var rolesDto = new List<RolesDto>();
        foreach (var item in roles)
        {
            rolesDto.Add( new RolesDto { Name=item.Name,Id=item.Id});
        }
        return rolesDto;
    }

    #endregion

}
