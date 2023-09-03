using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using CommandService.Data;
using CommandService.Data.Entities;
using CommandService.Data.Repo;
using CommandService.Services;
using CommandService.Concrete.EventProcessing;
using CommandService.Services.AsyncDataService;
using CommandService.Services.SyncDataService.Grpc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options
    .UseInMemoryDatabase("Command_InMemDb"));

builder.Services.AddScoped<IRepo<Platform>, PlatformRepo>();
builder.Services.AddScoped<IRepo<Command>, CommandRepo>();
builder.Services.AddScoped<IPlatformService, PlatformService>();
builder.Services.AddScoped<ICommandService, CommandService.Services.CommandService>();

builder.Services.AddSingleton<IEventProcessor, EventProcessor>();
builder.Services.AddSingleton<IPlatfromGrpcService, PlatfromGrpcService>();

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddHostedService<MessageBusSubscriber>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

Thread.Sleep(TimeSpan.FromSeconds(30));
PrepData.PrepPopulation(app);

app.Run();