using PlatformService.Dtos;

namespace PlatformService.Services;

public interface IPlatformService
{
    IReadOnlyCollection<PlatformReadDtos> GetAll();
    PlatformReadDtos? Get(int id);
    PlatformReadDtos? Create(PlatformCreateDtos entity);
    void Update(PlatformReadDtos entity);
    void Delete(int id);
}
