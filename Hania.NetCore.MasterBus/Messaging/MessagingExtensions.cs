using System;
using Hania.NetCore.MasterBus.Core;
using Hania.NetCore.MasterBus.Core.Commands;
using Hania.NetCore.MasterBus.Core.Events;
using Hania.NetCore.MasterBus.EventSourcing.Builders;
using Hania.NetCore.MasterBus.EventSourcing.Enums;
using Hania.NetCore.MasterBus.EventSourcing.Stores.EFCore;
using Hania.NetCore.MasterBus.EventSourcing.Stores.Mongo;
using Hania.NetCore.MasterBus.Messaging.Core.Builders;
using Hania.NetCore.MasterBus.Messaging.Enums;
using Hania.NetCore.MasterBus.Messaging.MessageBrokers.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit.DependencyInjection.ServiceCollection;

namespace Hania.NetCore.MasterBus.Messaging
{
    public static class MessagingExtensions
    {
        public static IServiceCollection AddMessaging(this IServiceCollection services, Action<IMessagingOptionsBuilder> optionsBuilder)
        {
            var builder = new MessagingOptionsBuilder();
            optionsBuilder.Invoke(builder);

            var option = builder.Build();

            services.AddSingleton(option);
            
            if (option.MessageBrokerType.Value==MessageBrokerType.RabbitMQ.Value)
            {
                services.AddRabbitMessaging(option.RabbitMqOptions);
            }
            else if (option.MessageBrokerType.Value==MessageBrokerType.Kafka.Value)
            {
                // Todo : Add Kafka Service   
            }

            services.AddTransient<IMessageConsumer, IdempotentConsumer>();
            services.AddTransient<IEventBus, EventBus>();
            services.AddTransient<ICommandBus, CommandBus>();
            return services;
        } 
    }
}