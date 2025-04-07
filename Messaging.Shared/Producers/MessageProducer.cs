using Messaging.Shared.Interfaces;
using Messaging.Shared.Policies;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Messaging.Shared.Producers
{
    public class MessageProducer : IMessageProducer
    {
        private readonly IMessagingConnection _connection;
        private readonly ILogger<MessageProducer> _logger;

        public MessageProducer(IMessagingConnection connection, ILogger<MessageProducer> logger)
        {
            _connection = connection;
            _logger = logger;
        }

#pragma warning disable
        public async void SendMessage<T>(T message, string queueName)
        {

            RetryPolicies.GetBasicRetryPolicy("MessageProducer.SendMessage").Execute(async () =>
            {
                using var channel = await _connection.GetConnection().CreateChannelAsync();
                channel?.QueueDeclareAsync(queue: queueName, 
                    durable: false,
                    exclusive: false, 
                    autoDelete: false);

                var json = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublishAsync(exchange: "", routingKey: queueName, body: body);

                _logger.LogInformation("Message sent to queue '{Queue}': {Message}", queueName, json);
            });
        }
    }
#pragma warning restore
}
