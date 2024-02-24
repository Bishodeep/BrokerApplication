using clean.Application.Contracts.Persistance;
using clean.Application.Contracts.Services;
using clean.Infrastructure.Identity;
using clean.Infrastructure.Models;
using clean.Infrastructure.Persistence;
using clean.Infrastructure.Persistence.Repositories;
using clean.Infrastructure.Services;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace clean.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));
        services.AddOptions<JwtIssuerOptions>()
             .Configure<IConfiguration>((settings, configuration) =>
             {
                 configuration.GetSection("JwtIssuerOptions").Bind(settings);
             });


        services
            .AddIdentity<ApplicationUser,ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddScoped<IPropertyRepository, PropertyRespository>();
        services.AddTransient<ActionControlFilterAttribute>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        return services;
    }
}
