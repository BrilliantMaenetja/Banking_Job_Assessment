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
                HostName = "localhost", // Configurable
                UserName = "guest",
                Password = "guest"
            };

            _connection = (IConnection)factory.CreateConnectionAsync();

            if (_connection == null)
            {
                throw new Exception("Failed to create RabbitMQ connection");
            }
        }

        
    }
}
