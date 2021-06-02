using System;
using System.Collections.Generic;
using System.Text;
using Hania.NetCore.MasterBus.Core.Messaging;
using RabbitMQ.Client.Events;

namespace Hania.NetCore.MasterBus.Messaging.MessageBrokers.RabbitMQ.Extensions
{
    public static class BasicDeliverEventArgsExtensions
    {
        public static MessagePacket ToMessagePacket(this BasicDeliverEventArgs ea)
        {
            
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            if (ea?.BasicProperties != null)
                return new MessagePacket()
                {
                    MessageBody = message,
                    CorrelationId = Guid.Parse(ea?.BasicProperties?.CorrelationId),
                    Headers = (Dictionary<string, object>) ea?.BasicProperties?.Headers,
                    MessageName = ea?.BasicProperties?.Type,
                    MessageId = Guid.Parse(ea?.BasicProperties.MessageId),
                    Route = ea.RoutingKey
                };

            return null;
        }
    }
}