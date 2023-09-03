using PlatformService.Dtos;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace PlatformService.Services.AsyncDataService;

public class MessageBusClient : IMessageBusClient
{
    private readonly IConfiguration config;
    private IConnection _connection;
    private IModel _channel;

    public MessageBusClient(IConfiguration config)
    {
        this.config = config;
        var factory = new ConnectionFactory()
        {
            HostName = config["RabbitMQConfig:Host"],
            Port = int.Parse(config["RabbitMQConfig:Port"])
        };

        try
        {
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

            _connection.ConnectionShutdown += new EventHandler<ShutdownEventArgs>(RabbitMQ_Connection_Shubdown);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to Connect to Message Bus : {ex}");
        }
    }

    private void RabbitMQ_Connection_Shubdown(object? sender, ShutdownEventArgs e)
    {
        Console.WriteLine("RabbitMQ Message Bus Shutdown.");
    }

    public void PublishNewPlatform(PlatformPublishedDtos publishedPlatform)
    {
        if (!_connection.IsOpen)
        {
            Console.WriteLine("RabbitMQ Message Bus Connection Close, Message Discarded");
            return;
        }

        var message = JsonSerializer.Serialize(publishedPlatform);

        Console.WriteLine("RabbitMQ Message Bus Connection Open, Sending Message to Bus.....");
        SendMessage(message);
    }

    private void SendMessage(string message)
    {
        var byteMessage = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(exchange: "trigger", routingKey: "",
            basicProperties: null, body: byteMessage);

        Console.WriteLine($"Published Message Sent : {message}");
    }

    public void Dispose()
    {
        if (_channel != null && _channel.IsOpen)
        {
            _channel.Close();
        }

        if (_connection != null && _connection.IsOpen)
        {
            _connection.Close();
        }

        _channel?.Dispose();
        _connection?.Dispose();
    }
}
