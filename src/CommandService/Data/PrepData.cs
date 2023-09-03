using CommandService.Data.Entities;
using CommandService.Dtos;
using CommandService.Services;
using CommandService.Services.SyncDataService.Grpc;

namespace CommandService.Data;

public static class PrepData
{
    public static void PrepPopulation(IApplicationBuilder app)
    {
        using (var serviceScoped = app.ApplicationServices.CreateScope())
        {
            var grpcClient = serviceScoped.ServiceProvider.GetService<IPlatfromGrpcService>();
            var allPlatfroms = grpcClient.GetPlatforms();

            if (allPlatfroms != null && allPlatfroms.Count >= 1)
            {
                var platformService = serviceScoped.ServiceProvider.GetService<IPlatformService>();
                SeedData(platformService, allPlatfroms);
            }
        }
    }

    private static void SeedData(IPlatformService service, IReadOnlyCollection<PlatformCreateDtos> platforms)
    {
        if (platforms != null && platforms.Count >= 1)
        {
            Console.WriteLine("Seeding Data");

            foreach (var platform in platforms)
            {
                if (!service.IsExtenalIdExist(platform.EnternalId))
                {
                    service.Create(platform);
                }
            }

            Console.WriteLine("Seeded Data");
        }
    }
}
