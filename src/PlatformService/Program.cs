using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.Data.Entities;
using PlatformService.Data.Repo;
using PlatformService.Services;
using PlatformService.Services.AsyncDataService;
using PlatformService.Services.Http;
using PlatformService.Services.SyncDataService.Grpc;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    Console.WriteLine("Using InMem Db");
    builder.Services.AddDbContext<AppDbContext>(options => options
        .UseInMemoryDatabase("Platform_InMemDb"));
}
else
{
    Console.WriteLine("Using Sql Server Db");
    builder.Services.AddDbContext<AppDbContext>(options => options
       .UseSqlServer(builder.Configuration.GetConnectionString("MicrosoftsCons")));
}

builder.Services.AddScoped<IRepo<Platform>, PlatformRepo>();
builder.Services.AddScoped<IPlatformService, PlatformService.Services.PlatformService>();

//builder.Services.AddHttpClient("CommandService").ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
//{
//    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
//});

builder.Services.AddHttpClient<ICommandServiceClient, CommandServiceClient>(
    serviceProvider => new CommandServiceClient(
            configuration: builder.Configuration,
            clientFactory: new HttpClient(new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            })
    )
);

builder.Services.AddScoped<IMessageBusClient, MessageBusClient>();


// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddControllers();

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

app.UseRouting();

app.UseAuthorization();

//app.MapControllers();

app.UseEndpoints(endpoints => 
{
    endpoints.MapControllers();
    endpoints.MapGrpcService<GrpcPlatfromService>();

    endpoints.MapGet("Protos/Platforms.Proto", async context =>
    {
        context.Response.StatusCode = 200;
        await context.Response.WriteAsync(File.ReadAllText("Protos/platforms.proto"));
    });
});

PrepData.PrepPopulation(app, app.Environment.IsProduction());

Console.WriteLine($"Platfrom Service Start : {builder.Configuration["CommandServiceUrl"]}");

app.Run();