using Application.Common.Exceptions;
using clean.Application.Common.Models;
using clean.Application.Contracts.Persistance;
using clean.Application.Features.Property.Requests;
using MediatR;

namespace clean.Application.Features.Property.Command
{
    public class DeletePropertyCommandHandler : IRequestHandler<DeletePropertyCommand, ResponseModel>
    {
        private readonly IPropertyRepository _propertyRepository;

        public DeletePropertyCommandHandler(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }
        public async Task<ResponseModel> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
        {
            var propertyToDelete = await _propertyRepository.GetSingleAsync(request.Id);
            if(propertyToDelete==null)
                throw new NotFoundException();
            await _propertyRepository.DeleteAsync(propertyToDelete);
            return new ResponseModel { Status = true, Message = "Property deleted successfully", StatusCode = 200 };
        }
    }
}
