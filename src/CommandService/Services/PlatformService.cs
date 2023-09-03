using AutoMapper;
using CommandService.Data.Entities;
using CommandService.Data.Repo;
using CommandService.Dtos;

namespace CommandService.Services
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

            throw new Exception("Failed to Create New Platfrom");
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public PlatformReadDtos? Get(int id)
        {
            var platfrom = repoService.Get(id);
            var platfromDto = mapper.Map<PlatformReadDtos>(platfrom);

            return platfromDto;
        }

        public bool IsExist(int id)
        {
            return repoService.IsExists(id);
        }

        public bool IsExtenalIdExist(int enternalId)
        {
            var isExists = repoService
                .IsExists(x => x.EnternalId == enternalId);

            return isExists;
        }

        public IReadOnlyCollection<PlatformReadDtos> GetAll()
        {
            var allPlatform = repoService.GetAll();
            var platfromDtos = mapper.Map<IReadOnlyCollection<PlatformReadDtos>>(allPlatform);

            return platfromDtos;
        }

        public void Update(PlatformUpdateDtos entity)
        {
            throw new NotImplementedException();
        }
    }
}
