using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Hania.NetCore.MasterBus.Outbox.Stores.Mongo.Document
{
    public class InboxDocument<TKey>
    {
        [BsonId]
        public ObjectId Id { get; set; }  
        
        [BsonElement("messageId")]
        public string MessageId { get; set; }  
        
        [BsonElement("correlationId")]
        public string CorrelationId { get; set; }  
  
        [BsonElement("service")]
        public string Service { get; set; }
        
        [BsonElement("receivedAt")]
        public DateTime ReceivedAt { get; set; }
        
    }
}