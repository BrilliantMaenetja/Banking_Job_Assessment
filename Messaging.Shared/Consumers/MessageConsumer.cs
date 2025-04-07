using Messaging.Shared.Interfaces;
using Messaging.Shared.Policies;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;

namespace Messaging.Shared.Consumers
{
    public class MessageConsumer : IMessageConsumer
    {
        private readonly IMessagingConnection _connection;
        private readonly ILogger<MessageConsumer> _logger;

        public MessageConsumer(IMessagingConnection connection, ILogger<MessageConsumer> logger)
        {
            _connection = connection;
            _logger = logger;
        }

        // Existing code...

        public void Consume<T>(string queueName, Action<T> onMessageReceived)
        {
            RetryPolicies.GetBasicRetryPolicy("MessageConsumer.Consume").Execute(async () =>
            {
                var channel = await _connection.GetConnection().CreateChannelAsync();
                channel?.QueueDeclareAsync(queue: queueName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false);

                var consumer = new AsyncEventingBasicConsumer(channel!);
                consumer.ReceivedAsync += async (sender, args) =>
                {
                    var body = args.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    _logger.LogInformation("Received message from queue '{Queue}': {Message}", queueName, message);

                    try
                    {
                        var deserializedMessage = JsonSerializer.Deserialize<T>(message);
                        onMessageReceived?.Invoke(deserializedMessage!);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error while processing message from queue '{Queue}'", queueName);
                        throw;
                    }
                };

                await channel!.BasicConsumeAsync(queue: queueName, autoAck: true, consumer: consumer);
                _logger.LogInformation("Started consuming messages from queue: {Queue}", queueName);
            });
        }

    }
}
