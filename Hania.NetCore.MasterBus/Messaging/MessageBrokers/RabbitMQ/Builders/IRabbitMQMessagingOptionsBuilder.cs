using System;
using System.Diagnostics.CodeAnalysis;
using Hania.NetCore.MasterBus.Messaging.MessageBrokers.RabbitMQ.Options;
using RabbitMQ.Client;

namespace Hania.NetCore.MasterBus.Messaging.MessageBrokers.RabbitMQ.Builders
{
    public interface IRabbitMQMessagingOptionsBuilder
    {


        IRabbitMQMessagingOptionsBuilder Host(string host);

        IRabbitMQMessagingOptionsBuilder Exchange(string name, string type = ExchangeType.Topic, bool durable = true,
            bool autoDelete = false);

        IRabbitMQMessagingOptionsBuilder Queue(string name, bool durable = true, bool autoDelete = false,
            bool exclusive = false);

        RabbitMQMessagingOptions Build();

    }
}