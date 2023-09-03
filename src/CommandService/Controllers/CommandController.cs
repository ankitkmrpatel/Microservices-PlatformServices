using CommandService.Data.Entities;
using CommandService.Data.Repo;
using CommandService.Dtos;
using CommandService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
    [Route("api/c/Platforms/{platformId:int}/[controller]")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        private readonly ICommandService commandService;

        public CommandController(ICommandService commandService)
        {
            this.commandService = commandService;
        }

        [HttpGet]
        public IActionResult GetAll(int platformId)
        {
            return Ok(commandService.GetAll(platformId));
        }

        [HttpGet("{id:int}", Name = "GetCommandUsingIdAndPlatformId")]
        public IActionResult GetById(int platformId, int id)
        {
            return Ok(commandService.Get(platformId, id));
        }

        [HttpPost]
        public IActionResult Create(int platformId, CommandCreateDtos command)
        {
            var commandDto = commandService.Create(platformId, command);

            return CreatedAtRoute("GetCommandUsingIdAndPlatformId",
                new { platformId, id = commandDto.Id }, 
                commandDto);
        }
    }
}
