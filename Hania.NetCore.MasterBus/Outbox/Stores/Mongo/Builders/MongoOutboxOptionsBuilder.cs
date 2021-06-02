using System;
using Hania.NetCore.MasterBus.EventSourcing.Stores.Mongo.Options;

namespace Hania.NetCore.MasterBus.Outbox.Stores.Mongo.Builders
{
    public class MongoOutboxOptionsBuilder
    {
        private readonly MongoOutboxOptions _option;

        public MongoOutboxOptionsBuilder()
        {
             _option = new MongoOutboxOptions();
        }

        public MongoOutboxOptionsBuilder Host(string host)
        {
           
            _option.Host = host;
            return this;
        }
        
        public MongoOutboxOptionsBuilder Database(string database)
        {
           
            _option.Database = database;
            return this;
        }
        
        public MongoOutboxOptionsBuilder Collection(string outboxCollection,string inboxCollection)
        {
           
            _option.InboxCollection = inboxCollection;
            _option.OutboxCollection = outboxCollection;
            return this;
        }
        
        public MongoOutboxOptions Build()
        {
            if (_option.Host==null)
            {
                throw  new Exception($"Mongo Outbox Option '{nameof(_option.Host)}' cannot be null");
            }
            
            if (_option.Database==null)
            {
                throw  new Exception($"Mongo Outbox Option '{nameof(_option.Database)}' cannot be null");
            }
            
            if (_option.OutboxCollection==null)
            {
                _option.OutboxCollection = "Outbox";
            }
            if (_option.InboxCollection==null)
            {
                _option.InboxCollection = "Inbox";
            }
            return _option;
        }
        
    }
}