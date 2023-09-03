namespace CommandService.Concrete.EventProcessing;

public interface IEventProcessor
{
    void Process(string message);
}
