using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hania.NetCore.MasterBus.Core.Events;

namespace Hania.NetCore.MasterBus.Core.EventSource
{
    public interface IEventStore
    {
        Task Store<TEvent>(TEvent @event) where TEvent: IEvent;
        Task<IEnumerable<TEvent>> Events<TEvent>(Guid correlationId) where TEvent: IEvent;
        Task<TEvent> Event<TEvent>(Guid id);
        IEventStore Empty { get; }
    }
}