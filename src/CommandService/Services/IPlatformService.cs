using CommandService.Dtos;

namespace CommandService.Services;

public interface IPlatformService
{
    IReadOnlyCollection<PlatformReadDtos> GetAll();
    PlatformReadDtos? Get(int id);
    PlatformReadDtos? Create(PlatformCreateDtos entity);
    bool IsExist(int id);
    bool IsExtenalIdExist(int enternalId);
    void Update(PlatformUpdateDtos entity);
    void Delete(int id);
}
