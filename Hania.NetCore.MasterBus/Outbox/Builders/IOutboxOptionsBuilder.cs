using System;
using Hania.NetCore.MasterBus.EventSourcing.Builders;
using Hania.NetCore.MasterBus.EventSourcing.Options;
using Hania.NetCore.MasterBus.EventSourcing.Stores.EFCore.Builders;
using Hania.NetCore.MasterBus.EventSourcing.Stores.Mongo.Builders;
using Hania.NetCore.MasterBus.Outbox.Options;
using Hania.NetCore.MasterBus.Outbox.Stores.Mongo.Builders;

namespace Hania.NetCore.MasterBus.Outbox.Builders
{
    public interface IOutboxOptionsBuilder
    {

        IOutboxOptionsBuilder UseMongo(Action<MongoOutboxOptionsBuilder> options);
        IOutboxOptionsBuilder Processor(TimeSpan period);

       

        OutboxOptions Build();
        
    }
}