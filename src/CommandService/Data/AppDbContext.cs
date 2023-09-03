﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensibility;
using CommandService.Data.Entities;
using CommandService.Data.Repo;

namespace CommandService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Platform> Platforms { get; set; }
    public DbSet<Command> Commands { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Platform>()
            .HasMany(x => x.Commands)
            .WithOne(x => x.Platform)
            .HasForeignKey(x => x.PlatformId);

        modelBuilder.Entity<Command>()
            .HasOne(x => x.Platform)
            .WithMany(x => x.Commands)
            .HasForeignKey(x => x.PlatformId);

        base.OnModelCreating(modelBuilder);
    }
}
