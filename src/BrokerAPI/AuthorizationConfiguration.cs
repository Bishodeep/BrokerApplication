using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

namespace BrokerAPI
{
    public static class AuthorizationConfiguration
    {
        public static IServiceCollection RegisterAuthorization(this IServiceCollection services,IConfiguration configuration)
        {
            services
            .AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(
                options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["JwtIssuerOptions:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(
                                configuration["JwtIssuerOptions:Secret"]
                            )
                        )
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = async (context) =>
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            context.Response.ContentType = "application/json";

                            // Ensure we always have an error and error description.
                            if (string.IsNullOrEmpty(context.Error))
                                context.Error = "Auth token required";
                            if (string.IsNullOrEmpty(context.ErrorDescription))
                                context.ErrorDescription = "This request requires a valid JWT access token to be provided";
                            var details = new ProblemDetails
                            {
                                Status = StatusCodes.Status401Unauthorized,
                                Title = context.Error,
                                Detail = context.ErrorDescription,
                                Type = "https://tools.ietf.org/html/rfc7235#section-3.1"
                            };

                            context.Response.ContentType = "application/problem+json";
                            await context.Response.WriteAsync(JsonSerializer.Serialize(details, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
                        }
                    };
                }
            );
            return services;
        }
    }
}
