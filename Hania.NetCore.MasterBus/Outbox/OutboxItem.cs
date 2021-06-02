using System;
using Hania.NetCore.MasterBus.EventSourcing.Enums;
using Hania.NetCore.MasterBus.Messaging.Enums;

namespace Hania.NetCore.MasterBus.Outbox
{
    public class OutboxItem
    {
        public OutboxItem()
        {
        }

        public OutboxItem(Guid messageId,Guid correlationId , Type type ,string messageType, string payload)
        {
            MessageId = messageId;
            CorrelationId = correlationId;
            Payload = payload;
            Name = type.Name;
            Type = messageType;
        }

        public Guid CorrelationId { get; private set; }
        public Guid MessageId { get; private set; }
        public string Payload { get; private set; }
        public string Type { get; private set; }
        public string Name { get; private set; }
        public DateTime? ProcessedAt { get; set; }
    }
}