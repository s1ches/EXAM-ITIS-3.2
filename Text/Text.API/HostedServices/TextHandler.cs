using Text.API.Data;
using Text.API.Services.Kafka.Consumer;

namespace Text.API.HostedServices;

public class TextHandler(IConsumer<TextModel> consumer) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var message in consumer.ConsumeMessagesAsync(stoppingToken))
        {
            DataContext.Texts.Add(new Data.Entities.Text
            {
                Id = Guid.NewGuid(),
                Content = message.Text,
                CreatedAt = message.Date.ToDateTime()
            });
        }
    }
}