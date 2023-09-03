using AutoMapper;
using CommandService.Data.Entities;
using CommandService.Data.Repo;
using CommandService.Dtos;

namespace CommandService.Services;

public class CommandService : ICommandService
{
    private readonly IRepo<Platform> platformRepo;
    private readonly IRepo<Command> commandRepo;
    private readonly IMapper mapper;

    public CommandService(IRepo<Platform> platformRepo, IRepo<Command> commandRepo, IMapper mapper)
    {
        this.platformRepo = platformRepo;
        this.commandRepo = commandRepo;
        this.mapper = mapper;
    }


    public CommandReadDtos? Create(int platfromId, CommandCreateDtos entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        if (!platformRepo.IsExists(platfromId))
            throw new ArgumentException("platform not found.");

        var command = mapper.Map<Command>(entity);
        command.PlatformId = platfromId;

        command = commandRepo.Create(command);

        if (commandRepo.SaveChanges())
        {
            var commandDto = mapper.Map<CommandReadDtos>(command);
            return commandDto;
        }

        return null;
    }

    public void Delete(int platfromId, int id)
    {
        _ = GetCommand(platfromId, id);
        commandRepo.Delete(id);
    }

    public CommandReadDtos? Get(int platfromId, int id)
    {
        var command = GetCommand(platfromId, id);
        var commandDto = mapper.Map<CommandReadDtos>(command);

        return commandDto;
    }

    private Command? GetCommand(int platfromId, int id)
    {
        var command = commandRepo.Get(x => x.PlatformId == platfromId && x.Id == id);
        _ = command ?? throw new ArgumentException("Command not found.");

        return command;
    }

    public IReadOnlyCollection<CommandReadDtos> GetAll(int platfromId)
    {
        var allPlatformCommands = commandRepo.GetAll(x => x.PlatformId == platfromId);
        var commandDtos = mapper.Map<IReadOnlyCollection<CommandReadDtos>>(allPlatformCommands);

        return commandDtos;
    }

    public void Update(int platfromId, CommandReadDtos entity)
    {
        throw new NotImplementedException();
    }
}
