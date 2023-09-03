using AutoMapper;
using CommandService.Data.Entities;
using CommandService.Dtos;

namespace CommandService.Profiles;

public class CommandProfile : Profile
{
    public CommandProfile()
    {
        CreateMap<CommandCreateDtos, Command>();
        CreateMap<Command, CommandReadDtos>();
    }
}
