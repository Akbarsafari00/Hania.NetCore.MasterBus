using System;
using System.Threading.Tasks;
using Hania.NetCore.MasterBus.Core.Messaging;

namespace Hania.NetCore.MasterBus.Core.Events
{
    public interface IEventDispatcher
    {
        Task Publish<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}