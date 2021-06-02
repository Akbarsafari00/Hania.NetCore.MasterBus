using System.Threading.Tasks;

namespace Hania.NetCore.MasterBus.Core.Events
{
    public interface IEventHandler<TEvent>
        where TEvent : IEvent
    {
        Task Handle(TEvent @event);
    }
}