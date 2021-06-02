using System;
using System.Reflection;
using Hania.NetCore.MasterBus.EventSourcing.Builders;
using Hania.NetCore.MasterBus.EventSourcing.Enums;
using Hania.NetCore.MasterBus.EventSourcing.Options;
using Hania.NetCore.MasterBus.EventSourcing.Stores.EFCore.Builders;
using Hania.NetCore.MasterBus.EventSourcing.Stores.Mongo.Builders;
using Hania.NetCore.MasterBus.Messaging.Core.Builders;
using Hania.NetCore.MasterBus.Outbox.Builders;

namespace Hania.NetCore.MasterBus.Builder
{
    public interface IHaniaMicroserviceBuilder
    {

        public IHaniaMicroserviceBuilder UseMessaging(Action<IMessagingOptionsBuilder> options);
        public IHaniaMicroserviceBuilder UseEventSourcing(Action<IEventSourcingOptionsBuilder> options);
        public IHaniaMicroserviceBuilder UseOutbox(Action<IOutboxOptionsBuilder> options);
        public IHaniaMicroserviceBuilder FromAssemblies(Assembly[] assemblies);

        public HaniaMicroServiceOption Build();
        
    }
}