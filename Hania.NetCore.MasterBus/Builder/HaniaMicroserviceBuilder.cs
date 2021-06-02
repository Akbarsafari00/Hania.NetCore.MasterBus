using System;
using System.Reflection;
using Hania.NetCore.MasterBus.Core.Commands;
using Hania.NetCore.MasterBus.Core.Events;
using Hania.NetCore.MasterBus.Core.Extensions;
using Hania.NetCore.MasterBus.EventSourcing.Builders;
using Hania.NetCore.MasterBus.EventSourcing.Enums;
using Hania.NetCore.MasterBus.EventSourcing.Options;
using Hania.NetCore.MasterBus.EventSourcing.Stores.EFCore.Builders;
using Hania.NetCore.MasterBus.EventSourcing.Stores.Mongo.Builders;
using Hania.NetCore.MasterBus.Messaging.Core.Builders;
using Hania.NetCore.MasterBus.Outbox.Builders;
using ICommand = System.Windows.Input.ICommand;

namespace Hania.NetCore.MasterBus.Builder
{
    public class HaniaMicroserviceBuilder:IHaniaMicroserviceBuilder
    {
        private readonly HaniaMicroServiceOption _option;

        public HaniaMicroserviceBuilder()
        {
             _option = new HaniaMicroServiceOption();
        }
        
   
        
        public IHaniaMicroserviceBuilder UseMessaging(Action<IMessagingOptionsBuilder> options)
        {
            _option.MessagingAction = options;  
            return this;
        }
        
        
        public IHaniaMicroserviceBuilder UseEventSourcing(Action<IEventSourcingOptionsBuilder> options)
        {
            _option.ActionEventSourcing = options;  
            return this;
        }

        public IHaniaMicroserviceBuilder UseOutbox(Action<IOutboxOptionsBuilder> options)
        {
            _option.ActionOutbox = options;  
            return this;
        }

        public IHaniaMicroserviceBuilder FromAssemblies(Assembly[] assemblies)
        {
            _option.SetAssembliesAndType(assemblies);
            return this;
        }
        
        public HaniaMicroServiceOption Build()
        {
            if (_option.Assemblies==null)
            {
                _option.SetAssembliesAndType(AppDomain.CurrentDomain.GetAssemblies());
            }
            return _option;
        }
        
    }
}