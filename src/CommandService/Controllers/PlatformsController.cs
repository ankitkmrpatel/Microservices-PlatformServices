using Microsoft.AspNetCore.Mvc;
using CommandService.Dtos;
using CommandService.Services;

namespace CommandService.Controllers;

[Route("api/c/[controller]")]
[ApiController]
public class PlatformsController : ControllerBase
{
    private readonly IPlatformService service;

    public PlatformsController(IPlatformService service)
    {
        this.service = service;
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

    //[HttpPost("", Name = "CreateNewPlatform")]
    //public IActionResult NewPlatform(PlatformCreateDtos createDtos)
    //{
    //    Console.WriteLine($"Request Received #{Request.Host.Host}: {createDtos.Name}");

    //    if (!ModelState.IsValid)
    //    {
    //        return BadRequest(new { Message = "Validation Failed" });
    //    }

    //    var entity = service.Create(createDtos);

    //    if (null != entity)
    //        return CreatedAtRoute("GetPlatformById", new { id = entity.Id}, entity);

    //    return StatusCode(500);
    //}
}
