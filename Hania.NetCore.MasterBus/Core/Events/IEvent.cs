using System;

namespace Hania.NetCore.MasterBus.Core.Events
{
    public interface IEvent
    {
        public Guid CorrelationId { get; set; }
    }
}