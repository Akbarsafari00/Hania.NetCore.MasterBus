using System;
using System.Threading.Tasks;
using Hania.NetCore.MasterBus.Core;
using Hania.NetCore.MasterBus.Core.Events;
using Hania.NetCore.MasterBus.Core.EventSource;
using Hania.NetCore.MasterBus.Core.Outbox;
using Hania.NetCore.MasterBus.Messaging.Enums;
using Hania.NetCore.MasterBus.Outbox;
using Newtonsoft.Json;

namespace Hania.NetCore.MasterBus.Messaging
{
    public class EventBus : IEventBus
    {

        private readonly IOutboxStore _outboxStore; 
        private readonly IEventStore _eventStore;
        private readonly IMessageBus _messageBus;
        private readonly IEventDispatcher _eventDispatcher;

        public EventBus(IOutboxStore outboxStore, IEventStore eventStore, IMessageBus messageBus, IEventDispatcher eventDispatcher)
        {
            _outboxStore = outboxStore;
            _eventStore = eventStore;
            _messageBus = messageBus;
            _eventDispatcher = eventDispatcher;
        }

        public async Task Publish<TEvent>(TEvent @event) where TEvent : IEvent
        {
            if (_eventStore!=null)
            {
                await _eventStore.Store(@event);
            }

            if (_outboxStore!=null)
            {
                var payload = JsonConvert.SerializeObject(@event);
                var item = new OutboxItem(Guid.NewGuid(), @event.CorrelationId,typeof(TEvent),MessageType.Event.Value,payload);
                await _outboxStore.StoreOutbox(item);
            }
            else if (_messageBus!=null)
            {
                await _messageBus.Publish(@event);
            }
            else
            {
               await _eventDispatcher.Publish(@event);
            }
            
        }

        public async Task PublishAll<TEvent>(TEvent[] @events) where TEvent : IEvent
        {
            foreach (var @event in events)
            {
                await Publish(@event);
            }
        }

        public Task Subscribe<TEvent>() where TEvent : IEvent
        {
           return _messageBus.Subscribe<TEvent>();
        }

        public Task Subscribe(Type type)
        {
           return _messageBus.Subscribe(type);
        }
    }
}