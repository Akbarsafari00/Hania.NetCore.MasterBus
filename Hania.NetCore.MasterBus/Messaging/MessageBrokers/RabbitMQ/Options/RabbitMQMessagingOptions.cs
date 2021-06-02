using RabbitMQ.Client;

namespace Hania.NetCore.MasterBus.Messaging.MessageBrokers.RabbitMQ.Options
{
    public class RabbitMQMessagingOptions
    {
        public string Host { get; set; }
        public RabbitMQExchangeMessagingOptions Exchange { get; set; }        
        public RabbitMQQueueMessagingOptions Queue { get; set; }        
        
    }

    public  class RabbitMQExchangeMessagingOptions
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool Durable { get; set; } 
        public bool AutoDelete { get; set; } 
    }
    
    public  class RabbitMQQueueMessagingOptions
    {
        public string Name { get; set; }
        public bool  Durable { get; set; }
        public bool  AutoDelete { get; set; } 
        public bool  Exclusive { get; set; } 
    }
}