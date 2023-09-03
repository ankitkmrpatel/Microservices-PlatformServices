using AutoMapper;
using Grpc.Core;
using PlatformService.Data.Entities;
using PlatformService.Data.Repo;
using PlatformService.Protos;

namespace PlatformService.Services.SyncDataService.Grpc;

public class GrpcPlatfromService : GrpcPlatfrom.GrpcPlatfromBase
{
    private readonly IRepo<Platform> platfromRepo;
    private readonly IMapper mapper;

    public GrpcPlatfromService(IRepo<Platform> platfromRepo, IMapper mapper)
    {
        this.platfromRepo = platfromRepo;
        this.mapper = mapper;
    }

    public override Task<PlatformResposneDto> GetAllPlatforms(GetAllRequest request, ServerCallContext context)
    {
        var response = new PlatformResposneDto();

        var platfroms = platfromRepo.GetAll();

        if (platfroms != null && platfroms.Count >= 1)
        {
            var grpcPlatfroms = mapper.Map<IReadOnlyCollection<GrpcPlatfromModel>>(platfroms);
            response.Platforms.AddRange(grpcPlatfroms);
        }

        return Task.FromResult(response);
    }
}
