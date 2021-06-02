namespace Hania.NetCore.MasterBus.EventSourcing.Stores.Mongo.Options
{
    public class MongoEventSourcingOptions
    {
        public string Host{ get; set; }
        public string Database{ get; set; }
        public string Collection{ get; set; }
    }
}