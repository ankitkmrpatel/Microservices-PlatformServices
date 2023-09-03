using System.ComponentModel.DataAnnotations;

namespace CommandService.Dtos;

public class PlatformCreateDtos
{
    [Required]
    public string Name { get; set; }

    [Required]
    public int EnternalId { get; set; }
}

public class PlatformUpdateDtos : PlatformCreateDtos
{
    [Required]
    public int Id { get; set; }
}

public class PlatformReadDtos
{
    public int Id { get; set; }
    public int EnternalId { get; set; }
    public string Name { get; set; }
}

public class PlatformPublishedDtos
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Event { get; set; }
}
