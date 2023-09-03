using CommandService.Concrete.EventProcessing;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace CommandService.Services.AsyncDataService;

public class MessageBusSubscriber : BackgroundService
{
    private readonly IConfiguration _config;
    private readonly IEventProcessor _eventProcessor;
    private IConnection _connection;
    private IModel _channel;
    private string _queueName;

    public MessageBusSubscriber(IConfiguration config, IEventProcessor eventProcessor)
    {
        this._config = config;
        this._eventProcessor = eventProcessor;
        InitializeRabbitMQ();
    }

    private void InitializeRabbitMQ()
    {
        var factory = new ConnectionFactory()
        {
            HostName = _config["RabbitMQConfig:Host"],
            Port = int.Parse(_config["RabbitMQConfig:Port"])
        };

        try
        {
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
            _queueName = _channel.QueueDeclare().QueueName;

            _channel.QueueBind(exchange: "trigger", 
                queue: _queueName, 
                routingKey: "");

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

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += new EventHandler<BasicDeliverEventArgs>(RabbitMQ_NewMessage_Received);
        
        _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

        return Task.CompletedTask;
    }

    private void RabbitMQ_NewMessage_Received(object? sender, BasicDeliverEventArgs e)
    {
        Console.WriteLine("Message Bus: New Message Received");

        var body = e.Body;
        var notificationMessage = Encoding.UTF8.GetString(body.ToArray());

        _eventProcessor.Process(notificationMessage);
    }

    public override void Dispose()
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

        base.Dispose();
    }
}
