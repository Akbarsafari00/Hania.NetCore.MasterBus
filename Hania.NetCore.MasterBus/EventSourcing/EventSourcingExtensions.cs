using System;
using System.Collections.Generic;
using Hania.NetCore.MasterBus.EventSourcing.Builders;
using Hania.NetCore.MasterBus.EventSourcing.Enums;
using Hania.NetCore.MasterBus.EventSourcing.Options;
using Hania.NetCore.MasterBus.EventSourcing.Stores.EFCore;
using Hania.NetCore.MasterBus.EventSourcing.Stores.Mongo;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Hania.NetCore.MasterBus.EventSourcing
{
    public static class EventSouringExtensions
    {
        public static IServiceCollection AddEventSourcing(this IServiceCollection services,Action<IEventSourcingOptionsBuilder> optionsBuilder)
        {
            
            var builder = new EventSourcingOptionsBuilder();
            optionsBuilder.Invoke(builder);

            var option = builder.Build();

            if (option.Type.Value.Equals(EventSourcingType.Mongo.Value))
            {
                services.AddMongoEventSourcing(option.MongoOptions);
            }
            else if (option.Type.Value.Equals(EventSourcingType.EfCore.Value))
            {
                services.AddEFCoreEventSourcing(option.EFCoreOptions);
            }
            
            return services;
        } 
    }
}