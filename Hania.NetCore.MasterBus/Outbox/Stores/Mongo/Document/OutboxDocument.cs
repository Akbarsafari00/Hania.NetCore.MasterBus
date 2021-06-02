using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Hania.NetCore.MasterBus.Outbox.Stores.Mongo.Document
{
    public class OutboxDocument<TKey>
    {
        [BsonId]  
        public ObjectId Id { get; set; }
        
        [BsonElement("messageId")]
        public string MessageId { get; set; }
        
        [BsonElement("correlationId")]
        public string CorrelationId { get; set; }  
  
        [BsonElement("service")]
        public string Service { get; set; }
        [BsonElement("fullName")]
        public string FullName { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("payload")]
        public string Payload { get; set; }
        [BsonElement("type")]
        public string Type { get; set; }  
        [BsonElement("storedAt")]
        public DateTime StoredAt { get; set; }
        [BsonElement("processedAt")]
        public DateTime? ProcessedAt { get; set; }
        
    }
}