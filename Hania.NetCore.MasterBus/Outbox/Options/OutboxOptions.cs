using System;
using Hania.NetCore.MasterBus.EventSourcing.Enums;
using Hania.NetCore.MasterBus.EventSourcing.Stores.EFCore.Options;
using Hania.NetCore.MasterBus.EventSourcing.Stores.Mongo.Builders;
using Hania.NetCore.MasterBus.EventSourcing.Stores.Mongo.Options;

namespace Hania.NetCore.MasterBus.Outbox.Options
{
    public class OutboxOptions
    {
        public OutboxType Type { get; set; }
        public MongoOutboxOptions MongoOptions { get; set; }
        public TimeSpan  ProcessorPeriod { get; set; }
    }
}