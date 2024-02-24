using clean.Application.Common.Models;
using clean.Application.Common.Models.Property;
using clean.Application.Features.Property.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrokerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PropertyController : ApiControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<PropertyDto>>> Get()
        {
            return await Mediator.Send(new GetPropertyQuery());
        }
        [HttpPost]
        public async Task<ActionResult<ResponseModel>> Create(PropertyDto propertyCreateDto)
        {
            return await Mediator.Send(new CreatePropertyCommand() { PropertyCreateDto = propertyCreateDto });
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseModel>> Update(string id,PropertyDto propertyCreateDto)
        {
            return await Mediator.Send(new UpdatePropertyCommand(id) { Property = propertyCreateDto });
        }
        [HttpDelete("id")]
        public async Task<ActionResult<ResponseModel>> Delete(string id)
        {
            return await Mediator.Send(new DeletePropertyCommand(id));
        }
    }
}
