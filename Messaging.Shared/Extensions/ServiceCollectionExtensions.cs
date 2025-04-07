using Messaging.Shared.Connection;
using Messaging.Shared.Consumers;
using Messaging.Shared.Interfaces;
using Messaging.Shared.Producers;
using Microsoft.Extensions.DependencyInjection;

namespace Messaging.Shared.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRabbitMQMessaging(this IServiceCollection services)
        {
            services.AddSingleton<IMessagingConnection, MessagingConnection>();
            services.AddTransient<IMessageProducer, MessageProducer>();
            services.AddTransient<IMessageConsumer, MessageConsumer>();
            

            return services;
        }
    }
}
