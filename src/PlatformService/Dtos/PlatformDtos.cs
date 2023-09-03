using System.ComponentModel.DataAnnotations;

namespace PlatformService.Dtos;

public class PlatformCreateDtos
{
    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }
    [Required]
    public string Publisher { get; set; }
    [Required] 
    public string Cost { get; set; }
}

public class PlatformReadDtos
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Publisher { get; set; }
    public string Cost { get; set; }
}

public class PlatformPublishedDtos
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Event { get; set; }
}
