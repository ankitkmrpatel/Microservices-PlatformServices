using CommandService.Dtos;

namespace CommandService.Services.SyncDataService.Grpc
{
    public interface IPlatfromGrpcService
    {
        IReadOnlyCollection<PlatformCreateDtos> GetPlatforms();
    }
}
