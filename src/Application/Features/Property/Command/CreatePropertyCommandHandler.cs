using AutoMapper;
using clean.Application.Common.Models;
using clean.Application.Contracts.Persistance;
using clean.Application.Features.Property.Requests;
using clean.Domain.Entities;
using MediatR;

namespace clean.Application.Features.Property.Command
{
    public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, ResponseModel>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;

        public CreatePropertyCommandHandler(IPropertyRepository propertyRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }
        public async Task<ResponseModel> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            await _propertyRepository.AddAsync(_mapper.Map<clean.Domain.Entities.Property>(request.PropertyCreateDto));
            return new ResponseModel { Status=true,Message="Property created successfully", StatusCode = 200 };
        }
    }
}
