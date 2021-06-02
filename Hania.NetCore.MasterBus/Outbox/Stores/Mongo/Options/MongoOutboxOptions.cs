namespace Hania.NetCore.MasterBus.EventSourcing.Stores.Mongo.Options
{
    public class MongoOutboxOptions
    {
        public string Host{ get; set; }
        public string Database{ get; set; }
        public string InboxCollection{ get; set; }
        public string OutboxCollection{ get; set; }
    }
}