using System;
using System.Linq;
using System.Threading.Tasks;
using Hania.NetCore.MasterBus.Core;
using Hania.NetCore.MasterBus.Core.Commands;
using Hania.NetCore.MasterBus.Core.Events;
using Hania.NetCore.MasterBus.Core.Messaging;
using Hania.NetCore.MasterBus.Core.Outbox;
using Hania.NetCore.MasterBus.Outbox;
using Newtonsoft.Json;

namespace Hania.NetCore.MasterBus.Messaging
{
    public class IdempotentConsumer : IMessageConsumer
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IOutboxStore _outboxStore;
        private readonly HaniaMicroServiceOption _option;

        public IdempotentConsumer(IEventDispatcher eventDispatcher, ICommandDispatcher commandDispatcher, IOutboxStore outboxStore, HaniaMicroServiceOption option)
        {
            _eventDispatcher = eventDispatcher;
            _commandDispatcher = commandDispatcher;
            _outboxStore = outboxStore;
            _option = option;
        }

        public async Task ConsumeEvent(string sender, MessagePacket packet)
        {
          
            
            if (_outboxStore!=null)
            {
                var canReceive = await _outboxStore.CanReceive(packet.MessageId);

                if (canReceive)
                {
                    var item = new InboxItem(packet.MessageId, packet.CorrelationId, sender);
                    await _outboxStore.StoreInbox(item);
                    
                    var eventType = _option.AllEventTypes.FirstOrDefault(x=>x.Name==packet.MessageName);
                    dynamic @event = JsonConvert.DeserializeObject(packet.MessageBody, eventType);
                    await _eventDispatcher.Publish(@event);
                   
                }
            }
            else
            {
                var eventType = _option.AllCommandTypes.FirstOrDefault(x=>x.Name==packet.MessageName);
                dynamic @event = JsonConvert.DeserializeObject(packet.MessageBody, eventType);
                await _eventDispatcher.Publish(@event);
            }
        }


        public async Task ConsumeCommand(string sender, MessagePacket packet)
        {
           
            
            if (_outboxStore!=null)
            {
                var canReceive = await _outboxStore.CanReceive(packet.MessageId);

                if (canReceive)
                {
                    var item = new InboxItem(packet.MessageId, packet.CorrelationId, sender);
                    await _outboxStore.StoreInbox(item);
                    var eventType = _option.AllCommandTypes.FirstOrDefault(x=>x.Name==packet.MessageName);
                    dynamic command = JsonConvert.DeserializeObject(packet.MessageBody, eventType);
                    await _commandDispatcher.Send(command);
                    
                }
            }
            else
            {
                var eventType = _option.AllCommandTypes.FirstOrDefault(x=>x.Name==packet.MessageName);
                dynamic command = JsonConvert.DeserializeObject(packet.MessageBody, eventType);
                await _commandDispatcher.Send(command);
            }
        }
    }
}