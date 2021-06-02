using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hania.NetCore.MasterBus.Core;
using Hania.NetCore.MasterBus.Core.Commands;
using Hania.NetCore.MasterBus.Core.Events;
using Hania.NetCore.MasterBus.Core.Extensions;
using Hania.NetCore.MasterBus.Core.Messaging;
using Hania.NetCore.MasterBus.Messaging.Enums;
using Hania.NetCore.MasterBus.Messaging.MessageBrokers.RabbitMQ.Extensions;
using Hania.NetCore.MasterBus.Messaging.MessageBrokers.RabbitMQ.Options;
using Hania.NetCore.MasterBus.Messaging.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Hania.NetCore.MasterBus.Messaging.MessageBrokers.RabbitMQ
{
    public class RabbitMQMessageBus : IMessageBus
    {
        
        private readonly RabbitMQMessagingOptions _messagingOptions;
        private readonly HaniaMicroServiceOption _options;
        private readonly IMessageConsumer _messageConsumer;

        private readonly IModel _channel;

        public RabbitMQMessageBus( RabbitMQMessagingOptions messagingOptions, IMessageConsumer messageConsumer, HaniaMicroServiceOption options , IRabbitMQProvider provider)
        {
            _messagingOptions = messagingOptions;
            _messageConsumer = messageConsumer;
            _options = options;
            _channel = provider.Channel();


            var exchangeName = _messagingOptions.Exchange.Name ?? _options.ServiceName;
            var queueName = _messagingOptions.Queue.Name ?? _options.ServiceName;
            
            _channel.ExchangeDeclare(exchangeName, _messagingOptions.Exchange.Type, _messagingOptions.Exchange.Durable, _messagingOptions.Exchange.AutoDelete);
            _channel.QueueDeclare(queueName, _messagingOptions.Queue.Durable, _messagingOptions.Queue.AutoDelete, _messagingOptions.Queue.Exclusive);
        }

       
        public Task Publish<TEvent>(TEvent @event) where TEvent : IEvent
        {
            var messageName = @event.GetType().Name;
            return SendMessagePacket(new MessagePacket()
            {
                MessageId = Guid.NewGuid(),
                CorrelationId = @event.CorrelationId,
                MessageBody = JsonConvert.SerializeObject(@event),
                MessageName = messageName,
                Route = $"{_options.ServiceName}.{messageName}"
            });
        }
        public Task Subscribe<TEvent>() where TEvent : IEvent
        {
            return Subscribe(typeof(TEvent));
        }
        public  Task Subscribe(Type type)
        {
            var route = $"{_options.ServiceName}.{type.Name}";
            return Task.Run(() =>
            {
                
                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += (s, ea) =>
                {
                    var packet = ea.ToMessagePacket();
                    _messageConsumer.ConsumeEvent(ea.BasicProperties.AppId,packet);
                };

                _channel.QueueBind(_messagingOptions.Queue.Name, _messagingOptions.Exchange.Name, route);
                _channel.BasicConsume(_messagingOptions.Queue.Name, true, consumer);
            });
        }

 

        public Task SendTo<TCommand>(string destinationService, string commandName, TCommand command) where TCommand : ICommand
        {
            return SendMessagePacket(new MessagePacket()
            {
                MessageId = Guid.NewGuid(),
                CorrelationId = command.CorrelationId,
                MessageBody = JsonConvert.SerializeObject(command),
                MessageName = typeof(TCommand).Name,
                Route =  $"{destinationService}.{commandName}"
            });
        }
        public Task Receive<TCommand>() where TCommand : ICommand
        {
            return Receive(typeof(TCommand));
        }
        public Task Receive(Type type)
        {  
            
            var route = $"{_options.ServiceName}.{type.Name}";
            return Task.Run(() =>
            {
               
                var consumer = new EventingBasicConsumer(_channel);
                
                consumer.Received += (s, ea) =>
                {
                    var packet = ea.ToMessagePacket();
                    _messageConsumer.ConsumeCommand(ea.BasicProperties.AppId,packet);
                };
                _channel.QueueBind(_messagingOptions.Queue.Name, _messagingOptions.Exchange.Name, route);
                _channel.BasicConsume(_messagingOptions.Queue.Name, true, consumer);
            });
        }
        public  Task SendMessagePacket(MessagePacket packet)
        {
            var exchangeName = $"{_options.ServiceName}.{packet.MessageName}";
            _channel.ExchangeDeclare(exchangeName,ExchangeType.Direct,_messagingOptions.Exchange.Durable,_messagingOptions.Exchange.AutoDelete);
            _channel.ExchangeBind(_messagingOptions.Exchange.Name,exchangeName,"");
            
            return  Task.Run(() =>
            {
                var basicProperties = _channel.CreateBasicProperties();
                basicProperties.AppId = _options.ServiceName;
                basicProperties.CorrelationId = packet?.CorrelationId.ToString();
                basicProperties.MessageId = packet?.MessageId.ToString();
                basicProperties.Headers = packet?.Headers;
                basicProperties.Type = packet?.MessageName;
                _channel.BasicPublish(_messagingOptions.Exchange.Name, packet?.Route, basicProperties, packet?.MessageBody.ToByteArray());
            });
        }
    }
}