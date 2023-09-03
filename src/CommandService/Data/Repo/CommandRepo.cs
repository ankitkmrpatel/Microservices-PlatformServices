using CommandService.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CommandService.Data.Repo;

public class CommandRepo : IRepo<Command>
{
    private readonly AppDbContext dbContext;

    public CommandRepo(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public Command Create(Command entity)
    {
        var command = dbContext.Commands.Add(entity);
        return command.Entity;
    }

    public void Delete(int id)
    {
        var command = Get(id);

        if (command != null)
        {
            dbContext.Commands.Remove(command);
        }
    }

    public Command? Get(int id)
    {
        var command = dbContext.Commands.AsNoTracking()
            .SingleOrDefault(x => x.Id.Equals(id));

        return command;
    }

    public Command? Get(Func<Command, bool> func)
    {
        var command = dbContext.Commands.AsNoTracking()
            .SingleOrDefault(func);

        return command;
    }

    public IReadOnlyCollection<Command> GetAll()
    {
        throw new NotImplementedException();
    }

    public IReadOnlyCollection<Command> GetAll(Func<Command, bool> func)
    {
        var commands = dbContext.Commands
            .AsNoTracking()
            .Where(func);

        return commands.ToList();
    }

    public bool IsExists(int id)
    {
        var isCommndExists = dbContext.Commands.AsNoTracking()
           .Any(x => x.Id.Equals(id));

        return isCommndExists;
    }

    public bool IsExists(Func<Command, bool> func)
    {
        throw new NotImplementedException();
    }

    public bool SaveChanges()
    {
        return dbContext.SaveChanges() >= 0;
    }

    public void Update(Command entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        var command = dbContext.Commands
            .SingleOrDefault(x => x.Id.Equals(entity.Id));

        if (command == null)
        {
            throw new ArgumentException(nameof(command));
        }

        command.HowTo = entity.HowTo;
        command.CommandLine = entity.CommandLine;
    }
}