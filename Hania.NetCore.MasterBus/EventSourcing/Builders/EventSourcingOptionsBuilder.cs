using System;
using System.Diagnostics.Tracing;
using Hania.NetCore.MasterBus.EventSourcing.Enums;
using Hania.NetCore.MasterBus.EventSourcing.Options;
using Hania.NetCore.MasterBus.EventSourcing.Stores.EFCore.Builders;
using Hania.NetCore.MasterBus.EventSourcing.Stores.Mongo.Builders;

namespace Hania.NetCore.MasterBus.EventSourcing.Builders
{
    public class EventSourcingOptionsBuilder:IEventSourcingOptionsBuilder
    {
        private readonly EventSourcingOptions _option;

        public EventSourcingOptionsBuilder()
        {
            
             _option = new EventSourcingOptions();
        }
        
        public IEventSourcingOptionsBuilder UseMongo(Action<MongoEventSourcingOptionsBuilder> options)
        {
            var builder = new MongoEventSourcingOptionsBuilder();
            options.Invoke(builder);

            var option = builder.Build();

            _option.Type = EventSourcingType.Mongo;
            _option.MongoOptions = option;
            
            return this;
        }
        
        public IEventSourcingOptionsBuilder UseEFCore(Action<EfCoreEventSourcingOptionsBuilder> options)
        {
            var builder = new EfCoreEventSourcingOptionsBuilder();
            options.Invoke(builder);

            var option = builder.Build();

            _option.Type = EventSourcingType.EfCore;
            _option.EFCoreOptions = option;
            
            return this;
        }
        
        public EventSourcingOptions Build()
        {
            if (_option.Type==null)
            {
                throw  new Exception($"EventSourcing Option '{nameof(_option.Type)}' cannot be null");
            }
            return _option;
        }
        
    }
}