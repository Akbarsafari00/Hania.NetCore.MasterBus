using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hania.NetCore.MasterBus.Core.Events;
using Hania.NetCore.MasterBus.Core.EventSource;
using Hania.NetCore.MasterBus.Core.Outbox;
using Hania.NetCore.MasterBus.EventSourcing.Stores.Mongo.Options;
using Hania.NetCore.MasterBus.Outbox.Stores.Mongo.Document;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace Hania.NetCore.MasterBus.Outbox.Stores.Mongo
{
    public class MongoOutboxStore : IOutboxStore
    {
        private readonly IMongoCollection<OutboxDocument<Guid>> _outboxCollection;
        private readonly IMongoCollection<InboxDocument<Guid>> _inboxCollection;
        private readonly HaniaMicroServiceOption _microServiceOption;

        public MongoOutboxStore(MongoOutboxOptions option, HaniaMicroServiceOption microServiceOption)
        {
            _microServiceOption = microServiceOption;
            try
            {
                var client = new MongoClient(option.Host);
                var database = client.GetDatabase(option.Database);
                _outboxCollection = database.GetCollection<OutboxDocument<Guid>>(option.OutboxCollection);
                _inboxCollection = database.GetCollection<InboxDocument<Guid>>(option.InboxCollection);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        public async Task StoreOutbox(OutboxItem item)
        {
            var document = new OutboxDocument<Guid>();
            document.MessageId = item.MessageId.ToString();
            document.CorrelationId = item.CorrelationId.ToString();
            document.Name = item.Name;
            document.Type = item.Type;
            document.Service = _microServiceOption.ServiceName;
            document.Payload = item.Payload;
            document.StoredAt = DateTime.UtcNow;
            document.ProcessedAt = null;
            await _outboxCollection.InsertOneAsync(document);
        }

        public async Task StoreInbox(InboxItem item)
        {
            var document = new InboxDocument<Guid>();
            document.MessageId = item.Id.ToString();
            document.CorrelationId = item.CorrelationId.ToString();
            document.Service = item.Service;
            document.ReceivedAt= DateTime.UtcNow;
            await _inboxCollection.InsertOneAsync(document);
        }

        public async Task SetMessageToProcessed(string id)
        {
            var filter = Builders<OutboxDocument<Guid>>.Filter.Where(d => d.MessageId == id);
            var update = Builders<OutboxDocument<Guid>>.Update.Set(x => x.ProcessedAt, DateTime.UtcNow);

            var result = await _outboxCollection.UpdateOneAsync(filter, update);

            if (result.ModifiedCount == 0)
            {
                throw new Exception($"Did not modify message '{id}'");
            }
        }

        public async Task<IEnumerable<OutboxDocument<Guid>>> GetAllUnProcessedItem()
        {
            var items =  _outboxCollection.Find(x => x.ProcessedAt == null);
            return items.ToEnumerable();
        }

        public async Task<bool> CanReceive(Guid messageId)
        {
            var find = await _inboxCollection.FindAsync(x => x.MessageId == messageId.ToString());
            var res =  !find.Any();
            return res;
        }
    }
}