using Microsoft.EntityFrameworkCore;
using CommandService.Data;
using CommandService.Data.Entities;

namespace CommandService.Data.Repo;

public class PlatformRepo : IRepo<Platform>
{
    private readonly AppDbContext dbContext;

    public PlatformRepo(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public Platform Create(Platform entity)
    {
        var platForm = dbContext.Platforms.Add(entity);
        return platForm.Entity;
    }

    public void Delete(int id)
    {
        var platform = Get(id);

        if (platform != null)
        {
            dbContext.Platforms.Remove(platform);
        }
    }

    public Platform? Get(int id)
    {
        var platForm = dbContext.Platforms.AsNoTracking()
            .SingleOrDefault(x => x.Id.Equals(id));

        return platForm;
    }

    public Platform? Get(Func<Platform, bool> func)
    {
        var platForm = dbContext.Platforms.AsNoTracking()
            .SingleOrDefault(func);

        return platForm;
    }

    public IReadOnlyCollection<Platform> GetAll()
    {
        var platForm = dbContext.Platforms
            .AsNoTracking();

        return platForm.ToList();
    }

    public IReadOnlyCollection<Platform> GetAll(Func<Platform, bool> func)
    {
        var platForm = dbContext.Platforms
            .AsNoTracking()
            .Where(func);

        return platForm.ToList();
    }

    public bool IsExists(int id)
    {
        var isPlatFormExists = dbContext.Platforms.AsNoTracking()
           .Any(x => x.Id.Equals(id));

        return isPlatFormExists;
    }

    public bool IsExists(Func<Platform, bool> func)
    {
        var isPlatFormExists = dbContext.Platforms.AsNoTracking()
           .Any(func);

        return isPlatFormExists;
    }

    public bool SaveChanges()
    {
        return dbContext.SaveChanges() >= 0;
    }

    public void Update(Platform entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        var platForm = dbContext.Platforms
            .SingleOrDefault(x => x.Id.Equals(entity.Id));

        if (platForm == null)
        {
            throw new ArgumentException(nameof(platForm));
        }

        platForm.Name = entity.Name;
    }
}
