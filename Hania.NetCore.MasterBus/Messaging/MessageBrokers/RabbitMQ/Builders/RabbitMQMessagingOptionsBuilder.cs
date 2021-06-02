using System;
using System.Diagnostics.CodeAnalysis;
using Hania.NetCore.MasterBus.Messaging.MessageBrokers.RabbitMQ.Options;
using RabbitMQ.Client;

namespace Hania.NetCore.MasterBus.Messaging.MessageBrokers.RabbitMQ.Builders
{
    public class RabbitMQMessagingOptionsBuilder : IRabbitMQMessagingOptionsBuilder
    {
        private readonly RabbitMQMessagingOptions _option;

        public RabbitMQMessagingOptionsBuilder()
        {
             _option = new RabbitMQMessagingOptions();
        }

        public IRabbitMQMessagingOptionsBuilder Host(string host )
        {
           
            _option.Host = host;
            return this;
        }
        
        public IRabbitMQMessagingOptionsBuilder Exchange(string name , string type = ExchangeType.Topic , bool durable = true , bool autoDelete = false )
        {
            _option.Exchange = new RabbitMQExchangeMessagingOptions()
            {
                Name = name,
                Durable = durable,
                Type =type,
                AutoDelete = autoDelete
            };        
            return this;
        }
        
        public IRabbitMQMessagingOptionsBuilder Queue(string name  , bool durable = true , bool autoDelete = false, bool exclusive = false )
        {
            _option.Queue = new RabbitMQQueueMessagingOptions()
            {
                Name = name,
                Durable = durable,
                Exclusive = exclusive,
                AutoDelete = autoDelete
            };        
            return this;
        }
        
        public RabbitMQMessagingOptions Build()
        {
            if (_option.Host==null)
            {
                throw  new Exception($"RabbitMq Messaging Option '{nameof(_option.Host)}' cannot be null");
            }
            
            return _option;
        }
        
    }
}