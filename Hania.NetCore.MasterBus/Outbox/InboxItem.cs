using System;
using Hania.NetCore.MasterBus.EventSourcing.Enums;
using Hania.NetCore.MasterBus.Messaging.Enums;

namespace Hania.NetCore.MasterBus.Outbox
{
    public class InboxItem
    {
        public InboxItem(Guid id, Guid correlationId, string service)
        {
            Id = id;
            CorrelationId = correlationId;
            Service = service;
        }

        public Guid Id { get; set; }
        public Guid CorrelationId { get; set; }
        public string Service { get; set; }
    }
}