using System;
using Hania.NetCore.MasterBus.Messaging.Enums;
using Hania.NetCore.MasterBus.Messaging.MessageBrokers.RabbitMQ.Builders;
using Hania.NetCore.MasterBus.Messaging.Options;
using RawRabbit.Configuration;

namespace Hania.NetCore.MasterBus.Messaging.Core.Builders
{
    public class MessagingOptionsBuilder : IMessagingOptionsBuilder
    {
        private readonly MessagingOptions _option;

        public MessagingOptionsBuilder()
        {
             _option = new MessagingOptions();
        }
        
      
        public IMessagingOptionsBuilder UseRabbitMQ(Action<IRabbitMQMessagingOptionsBuilder> options)
        {
            var builder = new RabbitMQMessagingOptionsBuilder();
            options.Invoke(builder);

            var option = builder.Build();
            
            _option.MessageBrokerType = MessageBrokerType.RabbitMQ;
            _option.RabbitMqOptions = option;
            
            return this;
        }
        
        public MessagingOptions Build()
        {
            if (_option.MessageBrokerType==null)
            {
                throw  new Exception($"Messaging Option '{nameof(_option.MessageBrokerType)}' cannot be null");
            }
            return _option;
        }
        
    }
}