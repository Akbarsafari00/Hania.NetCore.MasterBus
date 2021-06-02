using System;
using Hania.NetCore.MasterBus.EventSourcing.Builders;
using Hania.NetCore.MasterBus.EventSourcing.Enums;
using Hania.NetCore.MasterBus.EventSourcing.Options;
using Hania.NetCore.MasterBus.EventSourcing.Stores.Mongo.Builders;
using Hania.NetCore.MasterBus.Outbox.Options;
using Hania.NetCore.MasterBus.Outbox.Stores.Mongo.Builders;

namespace Hania.NetCore.MasterBus.Outbox.Builders
{
    public class OutboxOptionsBuilder:IOutboxOptionsBuilder{
        private readonly OutboxOptions _option;

        public OutboxOptionsBuilder()
        {
            
             _option = new OutboxOptions();
        }
        
        public IOutboxOptionsBuilder UseMongo(Action<MongoOutboxOptionsBuilder> options)
        {
            var builder = new MongoOutboxOptionsBuilder();
            options.Invoke(builder);

            var option = builder.Build();

            _option.Type = OutboxType.Mongo;
            _option.MongoOptions = option;
            
            return this;
        }

        public IOutboxOptionsBuilder Processor(TimeSpan period)
        {
            _option.ProcessorPeriod = period;
            return this;
        }


        public OutboxOptions Build()
        {
            if (_option.Type==null)
            {
                throw  new Exception($"EventSourcing Option '{nameof(_option.Type)}' cannot be null");
            }

            if (_option.ProcessorPeriod == TimeSpan.Zero)
            {
                _option.ProcessorPeriod = TimeSpan.FromSeconds(2);
            }
            return _option;
        }
        
    }
}