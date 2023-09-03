using PlatformService.Dtos;

namespace PlatformService.Services.AsyncDataService;

public interface IMessageBusClient
{
    void PublishNewPlatform(PlatformPublishedDtos publishedPlatform);
}
