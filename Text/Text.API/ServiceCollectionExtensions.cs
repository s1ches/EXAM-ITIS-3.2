using Text.API.HostedServices;
using Text.API.Services.Kafka.Consumer;
using Text.API.Services.Kafka.Producer;

namespace Text.API;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddLogging();

        services.AddScoped(typeof(IProducer<>), typeof(BaseProtobufProducer<>));
        services.AddSingleton(typeof(IConsumer<>), typeof(BaseProtobufConsumer<>));
        
        return services;
    }

    public static IServiceCollection AddWorkers(this IServiceCollection services)
    {
        services.AddHostedService<TextHandler>();
        return services;
    }
}