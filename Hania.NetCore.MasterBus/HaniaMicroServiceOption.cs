using System;
using System.Collections.Generic;
using System.Reflection;
using Hania.NetCore.MasterBus.Core.Commands;
using Hania.NetCore.MasterBus.Core.Events;
using Hania.NetCore.MasterBus.Core.Extensions;
using Hania.NetCore.MasterBus.EventSourcing.Builders;
using Hania.NetCore.MasterBus.Messaging.Core.Builders;
using Hania.NetCore.MasterBus.Outbox.Builders;

namespace Hania.NetCore.MasterBus
{
    public class HaniaMicroServiceOption
    {
        public string ServiceName  { get; set; }
        public Assembly[] Assemblies  { get; set; }
        public IEnumerable<Type> AllEventTypes  { get; set; }
        public IEnumerable<Type> AllCommandTypes  { get; set; }
        public Action<IMessagingOptionsBuilder> MessagingAction  { get; set; }
        
        public Action<IEventSourcingOptionsBuilder> ActionEventSourcing  { get; set; }
        public Action<IOutboxOptionsBuilder> ActionOutbox  { get; set; }

        public void SetAssembliesAndType(Assembly[] assemblies)
        {
            Assemblies = assemblies;
            AllCommandTypes = assemblies.GetAllImplementedTypes(typeof(ICommand));
            AllEventTypes = assemblies.GetAllImplementedTypes(typeof(IEvent));
        }
        
    }
}