using clean.Application.Common.Models;
using clean.Application.Common.Models.Property;
using clean.Application.Common.Security;
using MediatR;
using System.Data;

namespace clean.Application.Features.Property.Requests
{
    [Authorize(Roles = "Broker")]
    public record UpdatePropertyCommand(string Id):IRequest<ResponseModel>
    {
        public PropertyDto Property { get; set; }
    }
}
