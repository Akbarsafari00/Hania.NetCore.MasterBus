using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hania.NetCore.MasterBus.Core.Events;
using Hania.NetCore.MasterBus.Core.EventSource;
using Hania.NetCore.MasterBus.Core.Outbox;
using Hania.NetCore.MasterBus.EventSourcing.Options;
using Hania.NetCore.MasterBus.EventSourcing.Stores.Mongo.Document;
using Hania.NetCore.MasterBus.EventSourcing.Stores.Mongo.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace Hania.NetCore.MasterBus.EventSourcing.Stores.Mongo
{
    public class MongoStore: IEventStore
    {
        private readonly IMongoCollection<EventDocument<string>> _eventCollection;

        public MongoStore(MongoEventSourcingOptions option)
        {
            try
            {
 var client = new MongoClient(option.Host);  
            var database = client.GetDatabase(option.Database);  
            _eventCollection = database.GetCollection<EventDocument<string>>(option.Collection);
            
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        public async Task Store<TEvent>(TEvent @event) where TEvent: IEvent
        {
            var document = new EventDocument<string>();
            document.Id = Guid.NewGuid();
            document.CorrelationId = @event.CorrelationId.ToString();
            document.EventType = typeof(TEvent).Name;
            document.EventPayload = JsonConvert.SerializeObject(@event);
            document.StoredAt = DateTime.Now;
            await _eventCollection.InsertOneAsync(document);
        }

        public async  Task<IEnumerable<TEvent>> Events<TEvent>(Guid correlationId) where TEvent : IEvent
        {
            var res = await _eventCollection.FindAsync(c => c.CorrelationId.Equals(correlationId));
            var events =  res.ToEnumerable().Select(item => { return JsonConvert.DeserializeObject<TEvent>(item.EventPayload); });
            return events;
        }

        public async Task<TEvent> Event<TEvent>(Guid id)
        {
            var res = await _eventCollection.FindAsync(x => x.Id == id);
            var events =  res.ToEnumerable().Select(item => { return JsonConvert.DeserializeObject<TEvent>(item.EventPayload); });
            return events.FirstOrDefault();
        }

        public IEventStore Empty => null;
    }
}