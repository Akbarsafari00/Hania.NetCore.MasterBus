using System;
using System.Threading.Tasks;

namespace Hania.NetCore.MasterBus.Core.Events
{
    public interface IEventBus
    {
        Task Publish<TEvent>(TEvent @event) where TEvent : IEvent;
        Task PublishAll<TEvent>(TEvent[] @events) where TEvent : IEvent;
        Task Subscribe<TEvent>() where TEvent : IEvent;
        Task Subscribe(Type type);

    }
}