using System;
using Hania.NetCore.MasterBus.Core;
using Hania.NetCore.MasterBus.Core.Commands;
using Hania.NetCore.MasterBus.Core.Events;
using Hania.NetCore.MasterBus.Messaging.Core;
using Hania.NetCore.MasterBus.Messaging.Enums;
using Hania.NetCore.MasterBus.Messaging.MessageBrokers.RabbitMQ.HostedServices;
using Hania.NetCore.MasterBus.Messaging.MessageBrokers.RabbitMQ.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Hania.NetCore.MasterBus.Messaging.MessageBrokers.RabbitMQ
{
    internal static class RabbitMQMessagingExtensions
    {
        internal static IServiceCollection AddRabbitMessaging(this IServiceCollection services,RabbitMQMessagingOptions options)
        {
            services.AddSingleton(options);
            services.AddTransient<IRabbitMQProvider, RabbitMQProvider>();
            services.AddTransient<IMessageBus, RabbitMQMessageBus>();


            services.AddHostedService<RabbitMQHostedService>();
            return services;
        } 
    }
}