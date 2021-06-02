// ReSharper disable All
namespace Hania.NetCore.MasterBus.Messaging.Enums
{
    public class MessageBrokerType
    {
        private MessageBrokerType(string value) { Value = value; }
        public string Value { get; private set; }
        
        public static MessageBrokerType RabbitMQ   { get { return new MessageBrokerType("RabbitMQ"); } }
        public static MessageBrokerType Kafka   { get { return new MessageBrokerType("Kafka"); } }
    }
}