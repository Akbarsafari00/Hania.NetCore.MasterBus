using RabbitMQ.Client;

namespace Hania.NetCore.MasterBus.Messaging.MessageBrokers.RabbitMQ
{
    public interface IRabbitMQProvider
    {
        IConnection Connection();
        IModel Channel();
    }
}