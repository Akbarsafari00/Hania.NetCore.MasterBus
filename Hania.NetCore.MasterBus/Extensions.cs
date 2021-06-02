using System;
using Hania.NetCore.MasterBus.Builder;
using Hania.NetCore.MasterBus.Core;
using Hania.NetCore.MasterBus.EventSourcing;
using Hania.NetCore.MasterBus.EventSourcing.Builders;
using Hania.NetCore.MasterBus.EventSourcing.Enums;
using Hania.NetCore.MasterBus.EventSourcing.Stores.EFCore;
using Hania.NetCore.MasterBus.EventSourcing.Stores.Mongo;
using Hania.NetCore.MasterBus.Messaging;
using Hania.NetCore.MasterBus.Outbox;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Hania.NetCore.MasterBus
{
    public static class Extensions
    {
        public static IServiceCollection AddMicroservice(this IServiceCollection services,string serviceName ,Action<IHaniaMicroserviceBuilder> optionsBuilder)
        {
            
            var builder = new HaniaMicroserviceBuilder();
            optionsBuilder.Invoke(builder);

            var option = builder.Build();
            option.ServiceName = serviceName;
            
            services.AddSingleton(option);
            services.AddCore(option.Assemblies);
            if (option.ActionEventSourcing!=null)
            {
                services.AddEventSourcing(option.ActionEventSourcing);
            }
            if (option.MessagingAction!=null)
            {
                services.AddMessaging(option.MessagingAction);
            }
            if (option.ActionOutbox!=null)
            {
                services.AddOutbox(option.ActionOutbox);
            }
            return services;
        } 
    }
}