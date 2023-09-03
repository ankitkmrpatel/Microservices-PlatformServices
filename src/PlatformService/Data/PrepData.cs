using Microsoft.EntityFrameworkCore;
using PlatformService.Data.Entities;

namespace PlatformService.Data
{
    public static class PrepData
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using(var serviceScoped = app.ApplicationServices.CreateScope())
            {
                using (var dbContext = serviceScoped.ServiceProvider.GetService<AppDbContext>())
                {
                    SeedData(dbContext, isProd);
                }
            }
        }

        private static void SeedData(AppDbContext? dbContext, bool isProd)
        {
            if (dbContext != null)
            {
                if (isProd)
                {
                    try
                    {
                        Console.WriteLine("Applying Migration To Db");
                        dbContext.Database.Migrate();

                        Console.WriteLine("Applying Migration To Db Success");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Applying Migration To Db Failed {ex.Message}");
                    }
                }

                if (dbContext.Platforms.Any())
                {

                }
                else
                {
                    Console.WriteLine("Seeding Data");

                    dbContext.Platforms.AddRange(new List<Platform>() 
                    {
                        new Platform() { Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },   
                        new Platform() { Name = "SQL Server Express", Publisher = "Microsoft", Cost = "Free" },   
                        new Platform() { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free" },   
                    });

                    dbContext.SaveChanges();

                    Console.WriteLine("Seeded Data");
                }
            }
        }
    }
}
