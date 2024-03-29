﻿using System.ComponentModel.DataAnnotations;

namespace CommandService.Data.Entities;

public class Platform
{
    [Key]
    [Required] 
    public int Id { get; set; }

    [Required]
    public int EnternalId { get; set; }

    [Required]
    public string Name { get; set; }

    public ICollection<Command> Commands { get; set; } = new List<Command>();
}
