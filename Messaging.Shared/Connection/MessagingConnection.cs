using Messaging.Shared.Interfaces;
using RabbitMQ.Client;

namespace Messaging.Shared.Connection
{
    public class MessagingConnection : IMessagingConnection
    {
        private readonly IConnection _connection;

        public IConnection GetConnection() => _connection;

        public MessagingConnection()
        {
            ConnectionFactory factory = new()
            {
                HostName = "localhost", 
                Port = 5672, 
                UserName = "guest",
                Password = "guest"
            };

            // Await the asynchronous method to get the result and assign it to _connection
            _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();

            if (_connection == null)
            {
                throw new Exception("Failed to create RabbitMQ connection");
            }
        }
    }
}
