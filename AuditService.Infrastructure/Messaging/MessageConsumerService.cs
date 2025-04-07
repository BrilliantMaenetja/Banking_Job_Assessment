using AuditService.Application.DTOs;
using AuditService.Application.Services;
using Messaging.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditService.Infrastructure.Messaging
{
    public class MessageConsumerService : BackgroundService
    {
        private readonly IMessageConsumer _consumer;
        private readonly IAuditService _service;
        public MessageConsumerService(IMessageConsumer consumer, IAuditService service)
        {
            _consumer = consumer;
            _service = service;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var queues = new List<string>
            {
                "accountHolderQueue",
                "authenticationQueue",
                "bankAccountQueue",
                "transactionQueue"
            };

            foreach (var queue in queues)
            {
                _consumer.Consume<AccountHolderDTO>(queue, async (message) =>
                {
                    Console.WriteLine($"Received message from {queue}: {message}");

                    if (queue.Equals("accountHolderQueue"))
                    {
                        await _service.LogAction(message.Id, "ReceivedMessage");
                    }
                    else if (queue.Equals("authenticationQueue"))
                    {
                    }
                    else if (queue.Equals("bankAccountQueue"))
                    {

                    }
                    else if (queue.Equals("transactionQueue"))
                    {

                    }
                });
            }

            return Task.CompletedTask;
        }
    }
}                            
