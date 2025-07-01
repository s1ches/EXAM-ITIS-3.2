using System.Runtime.CompilerServices;
using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry.Serdes;
using Google.Protobuf;

namespace Text.API.Services.Kafka.Consumer;

public class BaseProtobufConsumer<TMessage>(IConfiguration configuration, ILogger<BaseProtobufConsumer<TMessage>> logger) 
    : IConsumer<TMessage>
    where TMessage : class, IMessage<TMessage>, new()
{
    private readonly ConsumerConfig _config = new()
    {
        BootstrapServers = configuration["Kafka:BootstrapServers"],
        AutoOffsetReset = AutoOffsetReset.Earliest,
        GroupId = "TextGroup"
    };
    
    public async IAsyncEnumerable<TMessage> ConsumeMessagesAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        using var consumer = new ConsumerBuilder<Null, TMessage>(_config)
            .SetValueDeserializer(new ProtobufDeserializer<TMessage>().AsSyncOverAsync())
            .Build();

        consumer.Subscribe(KafkaUtils.TopicName);

        while (!cancellationToken.IsCancellationRequested)
        {
            TMessage message;

            try
            {
                // Если не использовать Task.Run - приложение не запускается, а просто встаёт на моменте Consume
                // Более изящное решение - вынести HostedService в отдельное dotnet приложение
                var result = await Task.Run(() => consumer.Consume(cancellationToken), cancellationToken);

                if (result is null)
                    continue;

                if (result.IsPartitionEOF)
                {
                    logger.LogWarning("End of partition at {Date}", DateTime.UtcNow);
                    continue;
                }

                logger.LogInformation(
                    "Successful consume result");

                message = result.Message.Value;
            }
            catch (ConsumeException e)
            {
                if (e.Message.Contains(
                        $"Subscribed topic not available: {KafkaUtils.TopicName}: Broker: Unknown topic or partition"))
                {
                    logger.LogWarning(e.Message);
                    await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
                    continue;
                }
                else
                {
                    logger.LogError(e, "Error consuming message");
                    yield break;
                }
            }
            
            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            yield return message;
        }
    }
}