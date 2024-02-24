using clean.Application.Common.Models;
using clean.Application.Common.Security;
using MediatR;

namespace clean.Application.Features.Property.Requests
{
    [Authorize(Roles = "Broker")]
    public record DeletePropertyCommand(string Id):IRequest<ResponseModel>
    {
    }
}
