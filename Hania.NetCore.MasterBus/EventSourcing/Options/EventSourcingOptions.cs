using Hania.NetCore.MasterBus.EventSourcing.Enums;
using Hania.NetCore.MasterBus.EventSourcing.Stores.EFCore.Options;
using Hania.NetCore.MasterBus.EventSourcing.Stores.Mongo.Options;

namespace Hania.NetCore.MasterBus.EventSourcing.Options
{
    public class EventSourcingOptions
    {
        public EventSourcingType Type { get; set; }
        public MongoEventSourcingOptions MongoOptions { get; set; }
        public EfCoreEventSourcingOptions EFCoreOptions { get; set; }
    }
}