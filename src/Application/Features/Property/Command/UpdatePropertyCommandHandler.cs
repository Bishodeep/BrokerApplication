using Application.Common.Exceptions;
using clean.Application.Common.Models;
using clean.Application.Contracts.Persistance;
using clean.Application.Features.Property.Requests;
using MediatR;

namespace clean.Application.Features.Property.Command
{
    public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, ResponseModel>
    {
        private readonly IPropertyRepository _propertyRepository;

        public UpdatePropertyCommandHandler(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }
        public async Task<ResponseModel> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
            var propertyToUpdate = await _propertyRepository.GetSingleAsync(request.Id);
            if (propertyToUpdate == null)
                throw new NotFoundException();
            propertyToUpdate.Description = request.Property.Description;
            propertyToUpdate.OwnerName = request.Property.OwnerName;
            propertyToUpdate.OwnerContact = request.Property.OwnerContact;
            propertyToUpdate.Location = request.Property.Location;
            propertyToUpdate.Type = request.Property.Type;
            await _propertyRepository.UpdateAsync(propertyToUpdate);
            return new ResponseModel { Status = true, Message = "Property updated successfully",StatusCode=200 }; ;
        }
    }
}
