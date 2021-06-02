using System;
using Hania.NetCore.MasterBus.Core.EventSource;
using Hania.NetCore.MasterBus.EventSourcing.Builders;
using Hania.NetCore.MasterBus.EventSourcing.Options;
using Hania.NetCore.MasterBus.EventSourcing.Stores.Mongo.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Hania.NetCore.MasterBus.EventSourcing.Stores.Mongo
{
    public static class MongoEventSourcingExtensions
    {
        internal static IServiceCollection AddMongoEventSourcing(this IServiceCollection services,MongoEventSourcingOptions option )
        {
            services.AddSingleton(option);
            services.AddTransient<IEventStore, MongoStore>();
            return services;
        } 
    }
}