using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Hania.NetCore.MasterBus.EventSourcing.Stores.Mongo.Document
{
    public class EventDocument<TKey>
    {
        [BsonId]  
        public Guid Id { get; set; }  
  
        public string CorrelationId { get; set; }  
  
        public string EventType { get; set; }  
  
        public string EventPayload { get; set; }  
  
        public DateTime StoredAt { get; set; }  
    }
}