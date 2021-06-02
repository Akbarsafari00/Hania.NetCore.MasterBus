using System;
using Hania.NetCore.MasterBus.Messaging.Enums;
using Hania.NetCore.MasterBus.Messaging.MessageBrokers.RabbitMQ.Builders;
using Hania.NetCore.MasterBus.Messaging.Options;
using RawRabbit.Configuration;

namespace Hania.NetCore.MasterBus.Messaging.Core.Builders
{
    public interface IMessagingOptionsBuilder
    {
        IMessagingOptionsBuilder UseRabbitMQ(Action<IRabbitMQMessagingOptionsBuilder> options);
        MessagingOptions Build();
    }
}