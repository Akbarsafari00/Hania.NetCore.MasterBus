using System;
using System.Diagnostics.Tracing;
using Hania.NetCore.MasterBus.EventSourcing.Enums;
using Hania.NetCore.MasterBus.EventSourcing.Options;
using Hania.NetCore.MasterBus.EventSourcing.Stores.EFCore.Builders;
using Hania.NetCore.MasterBus.EventSourcing.Stores.Mongo.Builders;

namespace Hania.NetCore.MasterBus.EventSourcing.Builders
{
    public interface IEventSourcingOptionsBuilder
    {

        IEventSourcingOptionsBuilder UseMongo(Action<MongoEventSourcingOptionsBuilder> options);

        IEventSourcingOptionsBuilder UseEFCore(Action<EfCoreEventSourcingOptionsBuilder> options);

        EventSourcingOptions Build();
        
    }
}