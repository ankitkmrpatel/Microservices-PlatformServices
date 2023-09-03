using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensibility;
using PlatformService.Data.Entities;
using PlatformService.Data.Repo;

namespace PlatformService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Platform> Platforms { get; set; }

}
