using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Shared.Interfaces
{
    public interface IMessagingConnection
    {
        IConnection GetConnection();
    }
}
