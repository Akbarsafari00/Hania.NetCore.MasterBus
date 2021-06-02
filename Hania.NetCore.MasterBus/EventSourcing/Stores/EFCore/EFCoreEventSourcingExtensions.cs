using System;
using Hania.NetCore.MasterBus.EventSourcing.Options;
using Hania.NetCore.MasterBus.EventSourcing.Stores.EFCore.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Hania.NetCore.MasterBus.EventSourcing.Stores.EFCore
{
    public static class EFCoreEventSourcingExtensions
    {
        internal static IServiceCollection AddEFCoreEventSourcing(this IServiceCollection services,EfCoreEventSourcingOptions option )
        {
            services.AddSingleton(option);           
            return services;
        } 
    }
}