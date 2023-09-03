using AutoMapper;
using PlatformService.Data.Entities;
using PlatformService.Data.Repo;
using PlatformService.Dtos;

namespace PlatformService.Services
{
    public class PlatformService : IPlatformService
    {
        private readonly IRepo<Platform> repoService;
        private readonly IMapper mapper;

        public PlatformService(IRepo<Platform> repoService, IMapper mapper)
        {
            this.repoService = repoService;
            this.mapper = mapper;
        }

        public PlatformReadDtos? Create(PlatformCreateDtos entity)
        {
            if (null == entity) throw new ArgumentNullException(nameof(entity));
            var platform = mapper.Map<Platform>(entity);
            platform = repoService.Create(platform);

            if (repoService.SaveChanges())
            {
                var platformDto = mapper.Map<PlatformReadDtos>(platform);
                return platformDto;
            }

            return null;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public PlatformReadDtos? Get(int id)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<PlatformReadDtos> GetAll()
        {
            var allPlatform = repoService.GetAll();
            var platfromDto = mapper.Map<IReadOnlyCollection<PlatformReadDtos>>(allPlatform);

            return platfromDto;
        }

        public void Update(PlatformReadDtos entity)
        {
            if (null == entity) throw new ArgumentNullException(nameof(entity));

            var platform = mapper.Map<Platform>(entity);
            repoService.Update(platform);

            if (!repoService.SaveChanges())
            {
                throw new Exception("Failed to Update Platfrom");
            }
        }
    }
}
