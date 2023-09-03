using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Dtos;
using PlatformService.Services;
using PlatformService.Services.AsyncDataService;
using PlatformService.Services.Http;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformService service;
        private readonly IMapper mapper;
        private readonly ICommandServiceClient commandService;
        private readonly IMessageBusClient messageBusClient;

        public PlatformsController(IPlatformService service,
            IMapper mapper,
            ICommandServiceClient commandService,
            IMessageBusClient messageBusClient)
        {
            this.service = service;
            this.mapper = mapper;
            this.commandService = commandService;
            this.messageBusClient = messageBusClient;
        }

        [HttpGet("", Name = "GetAllPlatfrom")]
        public IActionResult Get()
        {
            return Ok(service.GetAll());
        }

        [HttpGet("{id:int}", Name = "GetPlatformById")]
        public IActionResult Get(int id)
        {
            var platform = service.Get(id);

            if (null != platform)
                return Ok(platform);

            return NotFound();
        }

        [HttpPost("", Name = "CreateNewPlatform")]
        public async Task<IActionResult> NewPlatform(PlatformCreateDtos createDtos)
        {
            Console.WriteLine($"Request Received #{Request.Host.Host}: {createDtos.Name}");

            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Validation Failed" });
            }

            var entity = service.Create(createDtos);

            if (null != entity)
            {
                //try
                //{
                //    await commandService.SendPlatform(entity);
                //}
                //catch (Exception e)
                //{
                //    Console.WriteLine("Failed to Send the Platfrom Entity to Command Service : {0}.", e);
                //}

                try
                {
                    var platform = mapper.Map<PlatformPublishedDtos>(entity);
                    platform.Event = "PlatformPublished";

                    messageBusClient.PublishNewPlatform(platform);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to Send the Platfrom Entity to Message Bus : {0}.", e);
                }

                return CreatedAtRoute("GetPlatformById", new { id = entity.Id }, entity);
            }

            return StatusCode(500);
        }
    }
}
