using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Hania.NetCore.MasterBus.Core;
using Hania.NetCore.MasterBus.Core.Events;
using Hania.NetCore.MasterBus.Core.Messaging;
using Hania.NetCore.MasterBus.Core.Outbox;
using Hania.NetCore.MasterBus.Messaging.Enums;
using Hania.NetCore.MasterBus.Outbox.Options;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Hania.NetCore.MasterBus.Outbox.HostedServices
{
    public class OutboxProcessor : IHostedService
    {
        private Timer _timer;
        private IOutboxStore _outboxStore;
        private IMessageBus _messageBus;
        private OutboxOptions _outboxOptions;
        private HaniaMicroServiceOption _option;

        public OutboxProcessor(IOutboxStore outboxStore, IMessageBus messageBus, OutboxOptions outboxOptions, HaniaMicroServiceOption option)
        {
            _outboxStore = outboxStore;
            _messageBus = messageBus;
            _outboxOptions = outboxOptions;
            _option = option;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(SendOutboxMessages, null, TimeSpan.Zero, _outboxOptions.ProcessorPeriod);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private void SendOutboxMessages(object state)
        {
            _ = Process();
        }

        public async Task Process()
        {
            var publishedMessageIds = new List<string>();
            try
            {
                var messages = await _outboxStore.GetAllUnProcessedItem();
                foreach (var message in messages)
                {
                    if (message is null || message.ProcessedAt.HasValue)
                    {
                        continue;
                    }

                    var packet = new MessagePacket
                    {
                        MessageBody = message.Payload,
                        MessageId = Guid.Parse(message.MessageId),
                        MessageName = message.Name,
                        CorrelationId = Guid.Parse(message.CorrelationId),
                        Route = $"{_option.ServiceName}.{message.Name}"
                    };

                    await _messageBus.SendMessagePacket(packet);
                    await _outboxStore.SetMessageToProcessed(message.MessageId);
                    publishedMessageIds.Add(message.MessageId);
                }
            }
            finally
            {
                // if (_outboxOptions.DeleteAfter)
                // {
                //     await store.Delete(publishedMessageIds);
                // }
            }
        }
    }
}