using clean.Application.Common.Models;
using clean.Application.Common.Models.Property;
using clean.Application.Common.Security;
using MediatR;

namespace clean.Application.Features.Property.Requests
{
    [Authorize(Roles ="Broker")]
    public record CreatePropertyCommand:IRequest<ResponseModel>
    {
        public PropertyDto PropertyCreateDto
        {
            get; set;
        }
    }
   
}
