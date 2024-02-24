using Application.Common.Exceptions;
using AutoMapper;
using clean.Application.Common.Models.Property;
using clean.Application.Contracts.Persistance;
using clean.Application.Features.Property.Command;
using clean.Application.Features.Property.Requests;
using Moq;
using NUnit.Framework;

namespace clean.Application.UnitTests.Feature.Property
{
    [TestFixture]
    public class PropertyCommandHandlersTests
    {
        private Mock<IPropertyRepository> _propertyRepositoryMock;
        private Mock<IMapper> _mockMapper;
        [SetUp]
        public async Task Setup()
        {
            _propertyRepositoryMock = new Mock<IPropertyRepository>();
            _mockMapper = new Mock<IMapper>();

            var props = new List<clean.Domain.Entities.Property>() {new clean.Domain.Entities.Property()
            {
                Type = "string123",
                Location = "string11",
                Description = "string",
                OwnerName = "string",
                OwnerContact = "string",
                Image = "string"
            } };
            _propertyRepositoryMock.Setup(x => x.GetAllPropertyAsync())
                .ReturnsAsync(props);
            _propertyRepositoryMock.Setup(x => x.AddAsync(It.IsAny<clean.Domain.Entities.Property>()))
                .Returns(Task.CompletedTask);
            _propertyRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<clean.Domain.Entities.Property>()))
               .Returns(Task.CompletedTask);
            _propertyRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<clean.Domain.Entities.Property>()))
               .Returns(Task.CompletedTask);

        }
        [Test]
        public async Task GivenValidProperty_ShouldSaveProperty()
        {
            var propertyDto = new PropertyDto()
            {
                Type = "string123",
                Location = "string11",
                Description = "string",
                OwnerName = "string",
                OwnerContact = "string",
                Image = "string"
            };
            _mockMapper.Setup(x => x.Map(It.IsAny<clean.Domain.Entities.Property>(), It.IsAny<PropertyDto>()))
               .Returns(new PropertyDto
               {
                   Type = "string123",
                   Location = "string11",
                   Description = "string",
                   OwnerName = "string",
                   OwnerContact = "string",
                   Image = "string"
               });
            _mockMapper.Setup(x => x.Map(It.IsAny<PropertyDto>(), It.IsAny<clean.Domain.Entities.Property>()))
                .Returns(new clean.Domain.Entities.Property()
                {
                    Type = "string123",
                    Location = "string11",
                    Description = "string",
                    OwnerName = "string",
                    OwnerContact = "string",
                    Image = "string"
                });
            var request = new CreatePropertyCommand() { PropertyCreateDto = propertyDto };
            var handler = new CreatePropertyCommandHandler(_propertyRepositoryMock.Object, _mockMapper.Object);
            await handler.Handle(request, CancellationToken.None);

            _propertyRepositoryMock.Verify(x => x.AddAsync(It.IsAny<clean.Domain.Entities.Property>()), Times.Once);

        }
        [Test]
        public async Task GivenValidProperty_ShouldUpdateProperty()
        {
            var propertyDto = new PropertyDto()
            {
                Type = "string123",
                Location = "string11",
                Description = "string",
                OwnerName = "string",
                OwnerContact = "string",
                Image = "string"
            };
            _propertyRepositoryMock.Setup(x => x.GetSingleAsync(It.IsAny<string>()))
               .ReturnsAsync(new clean.Domain.Entities.Property()
               {
                   Type = "string123",
                   Location = "string11",
                   Description = "string",
                   OwnerName = "string",
                   OwnerContact = "string",
                   Image = "string"
               });
            var request = new UpdatePropertyCommand("test") { Property = propertyDto };
            var handler = new UpdatePropertyCommandHandler(_propertyRepositoryMock.Object);
            var response = await handler.Handle(request, CancellationToken.None);

            Assert.AreEqual(200, response.StatusCode);

        }
        [Test]
        public void GivenInValidPropertyId_ShouldReturnNotFoundException()
        {
            var propertyDto = new PropertyDto()
            {
                Type = "string123",
                Location = "string11",
                Description = "string",
                OwnerName = "string",
                OwnerContact = "string",
                Image = "string"
            };
            _propertyRepositoryMock.Setup(x => x.GetSingleAsync(It.IsAny<string>()))
               .ReturnsAsync((clean.Domain.Entities.Property)null);
            var request = new UpdatePropertyCommand("test") { Property = propertyDto };
            var handler = new UpdatePropertyCommandHandler(_propertyRepositoryMock.Object);

            Assert.ThrowsAsync<NotFoundException>(async () => { await handler.Handle(request, CancellationToken.None); });

        }
        [Test]
        public void GivenInValidPropertyIdToDelete_ShouldReturnNotFoundException()
        {
            var propertyDto = new PropertyDto()
            {
                Type = "string123",
                Location = "string11",
                Description = "string",
                OwnerName = "string",
                OwnerContact = "string",
                Image = "string"
            };
            _propertyRepositoryMock.Setup(x => x.GetSingleAsync(It.IsAny<string>()))
               .ReturnsAsync((clean.Domain.Entities.Property)null);
            var request = new DeletePropertyCommand("test") {  };
            var handler = new DeletePropertyCommandHandler(_propertyRepositoryMock.Object);

            Assert.ThrowsAsync<NotFoundException>(async () => { await handler.Handle(request, CancellationToken.None); });

        }
        [Test]
        public async Task GivenValidPropertyIdToDelete_ShouldDeleteProperty()
        {
            var propertyDto = new PropertyDto()
            {
                Type = "string123",
                Location = "string11",
                Description = "string",
                OwnerName = "string",
                OwnerContact = "string",
                Image = "string"
            };
            _propertyRepositoryMock.Setup(x => x.GetSingleAsync(It.IsAny<string>()))
               .ReturnsAsync(new clean.Domain.Entities.Property()
               {
                   Type = "string123",
                   Location = "string11",
                   Description = "string",
                   OwnerName = "string",
                   OwnerContact = "string",
                   Image = "string"
               });
            var request = new DeletePropertyCommand("test") { };
            var handler = new DeletePropertyCommandHandler(_propertyRepositoryMock.Object);
            var response = await handler.Handle(request, CancellationToken.None);

            Assert.AreEqual(200, response.StatusCode);

        }
    }
}
