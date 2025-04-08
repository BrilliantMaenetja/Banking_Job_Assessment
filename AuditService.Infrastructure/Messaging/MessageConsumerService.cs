using AuditService.Application.DTOs;
using AuditService.Application.Services;
using Messaging.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
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
        //private readonly IAuditService _service;
        private readonly IServiceProvider _serviceProvider;
        public MessageConsumerService(IMessageConsumer consumer, IServiceProvider service)
        {
            _consumer = consumer;
            _serviceProvider = service;
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
                var scope = _serviceProvider.CreateScope();
                var _service = scope.ServiceProvider.GetRequiredService<IAuditService>();

                _consumer.Consume<AccountHolderDTO>(queue, async (message) =>
                {
                    Console.WriteLine($"Received message from {queue}: {message}");

                    if (queue.Equals("accountHolderQueue"))
                    {
                        await _service.LogAction(message.Id, message.Action!);
                    }
                    else if (queue.Equals("authenticationQueue"))
                    {
                        await _service.LogAction(message.Id, message.Action!);
                    }
                    else if (queue.Equals("bankAccountQueue"))
                    {
                        await _service.LogAction(message.Id, message.Action!);
                    }
                    else if (queue.Equals("transactionQueue"))
                    {
                        await _service.LogAction(message.Id, message.Action!);
                    }
                });
            }

            return Task.CompletedTask;

        }
    }
}                            
