using Hania.NetCore.MasterBus.EventSourcing.Enums;
using Hania.NetCore.MasterBus.EventSourcing.Options;
using Hania.NetCore.MasterBus.Messaging.Enums;
using Hania.NetCore.MasterBus.Messaging.MessageBrokers.RabbitMQ.Options;
using RawRabbit.Configuration;

namespace Hania.NetCore.MasterBus.Messaging.Options
{
    public class MessagingOptions
    {
        public MessageBrokerType MessageBrokerType { get; set; }
        public RabbitMQMessagingOptions RabbitMqOptions { get; set; }
        public RawRabbitConfiguration RawRabbitMqOptions { get; set; }
    }
}