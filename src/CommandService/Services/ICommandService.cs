using CommandService.Dtos;

namespace CommandService.Services;

public interface ICommandService
{
    IReadOnlyCollection<CommandReadDtos> GetAll(int platfromId);
    CommandReadDtos? Get(int platfromId, int id);
    CommandReadDtos? Create(int platfromId, CommandCreateDtos entity);
    void Update(int platfromId, CommandReadDtos entity);
    void Delete(int platfromId, int id);
}
