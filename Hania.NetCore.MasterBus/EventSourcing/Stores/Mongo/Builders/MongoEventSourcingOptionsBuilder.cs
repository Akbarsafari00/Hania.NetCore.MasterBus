using System;
using Hania.NetCore.MasterBus.EventSourcing.Options;
using Hania.NetCore.MasterBus.EventSourcing.Stores.Mongo.Options;

namespace Hania.NetCore.MasterBus.EventSourcing.Stores.Mongo.Builders
{
    public class MongoEventSourcingOptionsBuilder
    {
        private readonly MongoEventSourcingOptions _option;

        public MongoEventSourcingOptionsBuilder()
        {
             _option = new MongoEventSourcingOptions();
        }

        public MongoEventSourcingOptionsBuilder Host(string host)
        {
           
            _option.Host = host;
            return this;
        }
        
        public MongoEventSourcingOptionsBuilder Database(string database)
        {
           
            _option.Database = database;
            return this;
        }
        
        public MongoEventSourcingOptionsBuilder Collection(string collection)
        {
           
            _option.Collection = collection;
            return this;
        }
        
        public MongoEventSourcingOptions Build()
        {
            if (_option.Host==null)
            {
                throw  new Exception($"Mongo EventSourcing Option '{nameof(_option.Host)}' cannot be null");
            }
            
            if (_option.Database==null)
            {
                throw  new Exception($"Mongo EventSourcing Option '{nameof(_option.Collection)}' cannot be null");
            }
            
            if (_option.Collection==null)
            {
                _option.Collection = "EventSources";
            }
            return _option;
        }
        
    }
}