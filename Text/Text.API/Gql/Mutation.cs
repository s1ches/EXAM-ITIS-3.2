using Google.Protobuf.WellKnownTypes;
using Text.API.Services.Kafka;
using Text.API.Services.Kafka.Producer;

namespace Text.API.Gql;

// ReSharper disable once ClassNeverInstantiated.Global
public class Mutation
{
    // ReSharper disable once UnusedMember.Global
    public async Task<string> AddText([Service] IProducer<TextModel> producer, string text,
        CancellationToken cancellationToken = default)
    {
        var model = new TextModel { Text = text, Date = DateTime.UtcNow.ToTimestamp() };
        await producer.ProduceMessageAsync(model, KafkaUtils.TopicName, cancellationToken);
        return text; 
    }
}