using AutoMapper;
using CommandService.Data.Entities;
using CommandService.Dtos;
using CommandService.Protos;

namespace CommandService.Profiles;

public class PlatformProfile : Profile
{
    public PlatformProfile()
    {
        CreateMap<PlatformCreateDtos, Platform>();
        CreateMap<PlatformUpdateDtos, Platform>();
        CreateMap<Platform, PlatformReadDtos>();
        CreateMap<PlatformPublishedDtos, PlatformCreateDtos>()
            .ForMember(d => d.EnternalId, opt => opt.MapFrom(s => s.Id));
        CreateMap<PlatformPublishedDtos, PlatformUpdateDtos>()
            .ForMember(d => d.EnternalId, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.Id, opt => opt.Ignore());
        CreateMap<GrpcPlatfromModel, PlatformCreateDtos>()
            .ForMember(d => d.EnternalId, opt => opt.MapFrom(s => s.PlatfromId))
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
            ;
    }
}
