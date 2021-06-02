using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hania.NetCore.MasterBus.Core.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Hania.NetCore.MasterBus.Core.Events
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public EventDispatcher(IServiceProvider serviceProvider, HaniaMicroServiceOption option)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Publish<TEvent>(TEvent @event) where TEvent : IEvent
        {
            var handlers = _serviceProvider.GetServices<IEventHandler<TEvent>>();
            foreach (var handler in handlers)
            {
                await handler.Handle(@event);
            }
        }
    }
}