using AutoMapper;
using clean.Application.Common.Models.Property;
using clean.Application.Contracts.Persistance;
using clean.Application.Features.Property.Requests;
using MediatR;

namespace clean.Application.Features.Property.Queries
{
    public class GetPropertyQueryHandler : IRequestHandler<GetPropertyQuery, List<PropertyDto>>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;

        public GetPropertyQueryHandler(IPropertyRepository propertyRepository,IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }
        public async Task<List<PropertyDto>> Handle(GetPropertyQuery request, CancellationToken cancellationToken)
        {
            var properties = await _propertyRepository.GetAllPropertyAsync();
            return _mapper.Map<List<PropertyDto>>(properties);
        }
    }
}
