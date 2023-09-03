using AutoMapper;
using CommandService.Data.Entities;
using CommandService.Dtos;
using CommandService.Protos;
using Grpc.Net.Client;

namespace CommandService.Services.SyncDataService.Grpc;

public class PlatfromGrpcService : IPlatfromGrpcService
{
    private readonly IConfiguration configuration;
    private readonly IMapper mapper;

    public PlatfromGrpcService(IConfiguration configuration, IMapper mapper)
    {
        this.configuration = configuration;
        this.mapper = mapper;
    }


    public IReadOnlyCollection<PlatformCreateDtos> GetPlatforms()
    {
        Console.WriteLine($"Calling the Platfrom gRPC Service");

        var platformServiceUrl = configuration["ServiceCollectionConfig:PlatfromService"];
        _ = platformServiceUrl ?? throw new ArgumentException(nameof(platformServiceUrl));

        var platformGrpcServiceUrl = GrpcChannel.ForAddress(platformServiceUrl);
        var client = new GrpcPlatfrom.GrpcPlatfromClient(platformGrpcServiceUrl);

        try
        {
            var request = new GetAllRequest();
            var platforms = client.GetAllPlatforms(request);

            if (platforms?.Platforms != null && platforms.Platforms.Count > 0)
            {
                var allPlatforms = mapper.Map<IReadOnlyCollection<PlatformCreateDtos>>(platforms.Platforms);
                return allPlatforms;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to Call the Platfrom gRPC Service : {e}");
        }

        return new List<PlatformCreateDtos>();
    }
}
