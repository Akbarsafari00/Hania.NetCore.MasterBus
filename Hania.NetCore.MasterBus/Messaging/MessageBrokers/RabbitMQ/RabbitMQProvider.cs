using System;
using Hania.NetCore.MasterBus.Messaging.MessageBrokers.RabbitMQ.Options;
using RabbitMQ.Client;

namespace Hania.NetCore.MasterBus.Messaging.MessageBrokers.RabbitMQ
{
    public class RabbitMQProvider : IRabbitMQProvider
    {
        
        
        private  IModel _channel;
        private  IConnection _connection;
        private readonly RabbitMQMessagingOptions _messagingOptions;

        public RabbitMQProvider(RabbitMQMessagingOptions messagingOptions)
        {
            _messagingOptions = messagingOptions;
            _channel = CreateChannel();
        }
        
        private IModel CreateChannel()
        {
            _connection = CreateConnection();
            return _connection.CreateModel();
        }

        private IConnection CreateConnection()
        {
            var factory = new ConnectionFactory() { Uri = new Uri(_messagingOptions.Host) };
            return factory.CreateConnection();
        }
        
        public IConnection Connection()
        {
            return _connection;
        }

        public IModel Channel()
        {
            return _channel;
        }
    }
}