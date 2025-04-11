using BankAccountService.Application.Services;
using BankAccountService.Domain.Models;
using Messaging.Shared.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountService.Infrastructure.Messaging
{
    public class MessagingConsumerService : BackgroundService
    {
        private readonly IMessageConsumer _consumer;
        private readonly IServiceProvider _serviceProvider;
        public MessagingConsumerService(IMessageConsumer consumer, IServiceProvider service)
        {
            _consumer = consumer;
            _serviceProvider = service;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            var scope = _serviceProvider.CreateScope();
            var _service = scope.ServiceProvider.GetRequiredService<IBankService>();

            _consumer.Consume<BankAcount>(queueName: "accountHolderQueue", async (message) =>
            {
                Console.WriteLine($"Received message from AccountHolderQueue: {message}");
                // Processing the message

                _service?.CreateAccountAsync(message);


                await _service!.CreateAccountAsync(message);
            });


            return Task.CompletedTask;
        }
    }
}
