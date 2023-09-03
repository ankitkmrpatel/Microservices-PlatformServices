using PlatformService.Dtos;

namespace PlatformService.Services.Http
{
    public interface ICommandServiceClient
    {
        PlatformReadDtos GetPlatforms();
        Task SendPlatform(PlatformReadDtos platform);
    }
}
