using clean.Application.Common.Models.Property;
using MediatR;

namespace clean.Application.Features.Property.Requests
{
    public class GetPropertyQuery:IRequest<List<PropertyDto>>
    {
    }
}
