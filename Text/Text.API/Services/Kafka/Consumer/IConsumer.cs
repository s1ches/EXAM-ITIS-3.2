namespace Text.API.Services.Kafka.Consumer;

public interface IConsumer<out TMessage> where TMessage : new()
{
    IAsyncEnumerable<TMessage> ConsumeMessagesAsync(CancellationToken cancellationToken = default);
}