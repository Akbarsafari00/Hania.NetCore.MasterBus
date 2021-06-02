using System;
using System.Collections.Generic;

namespace Hania.NetCore.MasterBus.Core.Messaging
{
    public class MessagePacket
    {
        public Guid MessageId { get; set; }
        public Guid CorrelationId { get; set; }
        public string MessageName { get; set; }
        public string Route  { get; set; }
        public Dictionary<string, object> Headers { get; set; }
        public string MessageBody { get; set; }
    }
}