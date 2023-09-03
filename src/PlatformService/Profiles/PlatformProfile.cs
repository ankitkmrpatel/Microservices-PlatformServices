using AutoMapper;
using PlatformService.Data.Entities;
using PlatformService.Dtos;
using PlatformService.Protos;

namespace PlatformService.Profiles;

public class PlatformProfile : Profile
{
    public PlatformProfile()
    {
        CreateMap<PlatformCreateDtos, Platform>();
        CreateMap<Platform, PlatformReadDtos>();
        CreateMap<PlatformReadDtos, PlatformPublishedDtos>();
        CreateMap<Platform, GrpcPlatfromModel>()
            .ForMember(dest => dest.PlatfromId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            //.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher));
    }
}
