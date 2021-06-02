using System;
using Hania.NetCore.MasterBus.EventSourcing.Builders;
using Hania.NetCore.MasterBus.EventSourcing.Enums;
using Hania.NetCore.MasterBus.EventSourcing.Stores.EFCore;
using Hania.NetCore.MasterBus.EventSourcing.Stores.Mongo;
using Hania.NetCore.MasterBus.Outbox.Builders;
using Hania.NetCore.MasterBus.Outbox.HostedServices;
using Hania.NetCore.MasterBus.Outbox.Stores.Mongo;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Hania.NetCore.MasterBus.Outbox
{
    public static class EventSouringExtensions
    {
        public static IServiceCollection AddOutbox(this IServiceCollection services,Action<IOutboxOptionsBuilder> optionsBuilder)
        {
            
           
            
            var builder = new OutboxOptionsBuilder();
            optionsBuilder.Invoke(builder);
            
            var option = builder.Build();

            services.AddSingleton(option);
            
            if (option.Type.Value.Equals(OutboxType.Mongo.Value))
            {
                services.AddMongoOutbox(option.MongoOptions);
            }
            else if (option.Type.Value.Equals(EventSourcingType.EfCore.Value))
            {
            }

            services.AddHostedService<OutboxProcessor>();
            return services;
        } 
    }
}