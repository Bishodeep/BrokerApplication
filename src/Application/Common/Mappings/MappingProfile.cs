using System.Reflection;
using AutoMapper;
using clean.Application.Common.Models.Property;
using clean.Domain.Entities;

namespace clean.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Property, PropertyDto>().ReverseMap();
    }

}
