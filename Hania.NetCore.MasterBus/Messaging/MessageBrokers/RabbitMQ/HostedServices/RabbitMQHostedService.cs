#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Hania.NetCore.MasterBus.Core;
using Hania.NetCore.MasterBus.Core.Commands;
using Hania.NetCore.MasterBus.Core.Events;
using Hania.NetCore.MasterBus.Messaging.Core;
using Hania.NetCore.MasterBus.Messaging.Enums;
using Hania.NetCore.MasterBus.Messaging.MessageBrokers.RabbitMQ.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Hania.NetCore.MasterBus.Messaging.MessageBrokers.RabbitMQ.HostedServices
{
    public class RabbitMQHostedService : IHostedService
    {
        private readonly IModel _channel;
        private readonly IMessageConsumer _consumer;
        private readonly IMessageBus _messageBus;
        private readonly HaniaMicroServiceOption _option;
        private readonly RabbitMQMessagingOptions _rmqOptions;

        public RabbitMQHostedService(IRabbitMQProvider rmqProvider, HaniaMicroServiceOption option,
            IMessageConsumer consumer, RabbitMQMessagingOptions rmqOptions, IMessageBus messageBus)
        {
            _option = option;
            _consumer = consumer;
            _rmqOptions = rmqOptions;
            _messageBus = messageBus;
            _channel = rmqProvider.Channel();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var assembliesTypes = _option.Assemblies.SelectMany(x => x.GetTypes());
            var enumerable = assembliesTypes as Type[] ?? assembliesTypes.ToArray();
            await ConfigureConsumer(enumerable);
          
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _channel.Dispose();
        }

       
        private async Task ConfigureConsumer(IEnumerable<Type> assembliesTypes)
        {

            foreach (var command in _option.AllCommandTypes)
            {
                await _messageBus.Receive(command);
            }

            foreach (var @event in _option.AllEventTypes)
            {
                await _messageBus.Subscribe(@event);
            }
        }
       
    }
}