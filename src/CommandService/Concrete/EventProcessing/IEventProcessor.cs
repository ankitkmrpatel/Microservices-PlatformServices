using AutoMapper;
using CommandService.Data.Entities;
using CommandService.Dtos;
using CommandService.Services;
using System.Text.Json;

namespace CommandService.Concrete.EventProcessing;

public class EventProcessor : IEventProcessor
{
    private readonly IServiceScopeFactory scopeFactory;
    private readonly IMapper mapper;

    public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
    {
        this.scopeFactory = scopeFactory;
        this.mapper = mapper;
    }

    public void Process(string message)
    {
        var eventType = GetEventTypeUsingMessage(message);
        switch(eventType)
        {
            case EventType.PlatformPublished:
                ProcessPlatfrom(message);
                break;
        }
    }

    private void ProcessPlatfrom(string notificationMessage)
    {
        using(var scope = scopeFactory.CreateScope())
        {
            var repo = scope.ServiceProvider.GetRequiredService<IPlatformService>();
            var publishedPlatfrom = JsonSerializer.Deserialize<PlatformPublishedDtos>(notificationMessage);

            try
            {
                if (!repo.IsExtenalIdExist(publishedPlatfrom.Id))
                {
                    var platfromCreate = mapper.Map<PlatformCreateDtos>(publishedPlatfrom);
                    repo.Create(platfromCreate);
                }
                else
                {
                    var platfromUpdate = mapper.Map<PlatformUpdateDtos>(publishedPlatfrom);
                    repo.Update(platfromUpdate);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to Process Published Platfrom : #{e}");
            }
        }
    }

    private EventType GetEventTypeUsingMessage(string notificationMessage)
    {
        var message = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

        var eventType = message.Event switch
        {
            "PlatformPublished" => EventType.PlatformPublished,
            _ => EventType.Undetermined
        };

        return eventType;
    }
}

enum EventType
{
    PlatformPublished,
    Undetermined
}
