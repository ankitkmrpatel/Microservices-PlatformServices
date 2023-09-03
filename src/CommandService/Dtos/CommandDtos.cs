using System.ComponentModel.DataAnnotations;

namespace CommandService.Dtos;

public class CommandCreateDtos
{
    [Required]
    public string HowTo { get; set; }

    [Required]
    public string CommandLine { get; set; }
}

public class CommandReadDtos
{
    public int Id { get; set; }
    public string HowTo { get; set; }
    public string? CommandLine { get; set; }
}
