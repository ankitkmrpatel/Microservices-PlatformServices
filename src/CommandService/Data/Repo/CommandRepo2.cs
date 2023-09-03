using CommandService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommandService.Data.Repo;

public interface ICommandRepo
{
    IReadOnlyCollection<Command> GetAll(int platfromId);
    bool IsExists(int platfromId, int id);
    Command? Get(int platfromId, int id);
    Command Create(int platfromId, Command entity);
    void Update(int platfromId, Command entityty);
    void Delete(int platfromId, int id);
    bool SaveChanges();
}
public class CommandRepo2 : ICommandRepo
{
    private readonly AppDbContext dbContext;

    public CommandRepo2(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public Command Create(int platfromId, Command entity)
    {
        entity.PlatformId = platfromId;
        dbContext.Commands.Add(entity);

        return entity;
    }

    public void Delete(int platfromId, int id)
    {
        var command = Get(platfromId, id);
        if (command != null)
        {
            dbContext.Commands.Remove(command);
        }
    }

    public Command? Get(int platfromId, int id)
    {
        return dbContext.Commands
            .AsNoTracking()
            .SingleOrDefault(x => x.PlatformId.Equals(platfromId) &&
                x.Id.Equals(id));
    }

    public IReadOnlyCollection<Command> GetAll(int platfromId)
    {
        return dbContext.Commands
            .AsNoTracking()
            .Where(x => x.PlatformId.Equals(platfromId))
            .AsNoTracking()
            .ToList();
    }

    public bool IsExists(int platfromId, int id)
    {
        return dbContext.Commands
            .AsNoTracking()
            .Any(x => x.PlatformId.Equals(platfromId) &&
                x.Id.Equals(id));
    }

    public bool SaveChanges()
    {
        return dbContext.SaveChanges() >= 0;
    }

    public void Update(int platfromId, Command entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        var command = dbContext.Commands
            .SingleOrDefault(x => x.PlatformId.Equals(platfromId) &&
                x.Id.Equals(entity.Id));

        if (command == null)
        {
            throw new ArgumentException(nameof(entity));
        }

        command.HowTo = entity.HowTo;
        command.CommandLine = entity.CommandLine;
    }
}
