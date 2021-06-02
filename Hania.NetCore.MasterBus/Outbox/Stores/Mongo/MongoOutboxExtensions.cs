using System;
using Hania.NetCore.MasterBus.Core.Outbox;
using Hania.NetCore.MasterBus.EventSourcing.Stores.Mongo;
using Hania.NetCore.MasterBus.EventSourcing.Stores.Mongo.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Hania.NetCore.MasterBus.Outbox.Stores.Mongo
{
    public static class MongoOutboxExtensions
    {
        internal static IServiceCollection AddMongoOutbox(this IServiceCollection services,MongoOutboxOptions option )
        {
            services.AddSingleton(option);
            services.AddTransient<IOutboxStore, MongoOutboxStore>();

            return services;
        } 
    }
}