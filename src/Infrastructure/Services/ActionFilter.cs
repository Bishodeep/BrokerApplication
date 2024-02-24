using clean.Application.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace Infrastructure.Services;

public class ActionControlFilterAttribute : ActionFilterAttribute
{
    private readonly ICurrentUserService _currentUserService;

    public ActionControlFilterAttribute(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    //After Method execution
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        // Method intentionally left empty.
    }

    //Before Method execution
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();

        if (allowAnonymous)
        {
            await next();
            return;
        }

        var token = context.HttpContext.Request.Headers["Authorization"];
        if (AuthenticationHeaderValue.TryParse(token, out var headerValue))
        {
            var parameter = headerValue.Parameter;
            if (parameter != null)
            {
                var handler = new JwtSecurityTokenHandler();
                var decodedToken = handler.ReadToken(parameter) as JwtSecurityToken;
                var userId = decodedToken.Claims.FirstOrDefault(a => a.Type == "UserId")?.Value;
                context.HttpContext.Items.Add("UserId",userId);

                await next();
                return;
            }
        }
        context.Result = new UnauthorizedResult();
    }
}
