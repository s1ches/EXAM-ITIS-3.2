namespace Text.API.Services.Kafka.Producer;

public interface IProducer<in TMessage> where TMessage : new()
{
    Task ProduceMessageAsync(TMessage message, string topicName, CancellationToken cancellationToken = default);
}