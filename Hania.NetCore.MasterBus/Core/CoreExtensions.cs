using System;
using System.Collections.Generic;
using System.Reflection;
using Hania.NetCore.MasterBus.Builder;
using Hania.NetCore.MasterBus.Core.Commands;
using Hania.NetCore.MasterBus.Core.Events;
using Hania.NetCore.MasterBus.Core.EventSource;
using Hania.NetCore.MasterBus.Core.Outbox;
using Hania.NetCore.MasterBus.EventSourcing;
using Hania.NetCore.MasterBus.EventSourcing.Stores.Mongo;
using Microsoft.Extensions.DependencyInjection;

namespace Hania.NetCore.MasterBus.Core
{
    public static class CoreExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services , Assembly[] assemblies)
        {
            services.AddTransient<IMessageBus>((x) => null);
            services.AddTransient<IEventStore>((x) => null);
            services.AddTransient<IOutboxStore>((x) => null);
            services.AddTransient<ICommandDispatcher, CommandDispatcher>();
            services.AddTransient<IEventDispatcher, EventDispatcher>();
            services.AddEventHandlers(assemblies);
            services.AddCommandHandlers(assemblies);


            return services;
        } 
        
        private static IServiceCollection AddCommandHandlers(this IServiceCollection services,
            Assembly[] assemblies) =>
            services.AddWithTransientLifetime(assemblies, typeof(ICommandHandler<>), typeof(ICommandHandler<,>));
        
        public static IServiceCollection AddEventHandlers(this IServiceCollection services,Assembly[] assemblies)
        {
            services.AddWithTransientLifetime(assemblies, typeof(IEventHandler<>), typeof(IEventDispatcher));
            return services;
        } 
        
        
        
        
        public static IServiceCollection AddWithTransientLifetime(this IServiceCollection services,
            Assembly[] assemblies,
            params Type[] assignableTo)
        {
            services.Scan(s => s.FromAssemblies(assemblies)
                .AddClasses(c => c.AssignableToAny(assignableTo))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
            return services;
        }
        public static IServiceCollection AddWithScopedLifetime(this IServiceCollection services,
            Assembly[] assemblies,
            params Type[] assignableTo)
        {
            services.Scan(s => s.FromAssemblies(assemblies)
                .AddClasses(c => c.AssignableToAny(assignableTo))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
            return services;
        }

        public static IServiceCollection AddWithSingletonLifetime(this IServiceCollection services,
            Assembly[] assemblies,
            params Type[] assignableTo)
        {
            services.Scan(s => s.FromAssemblies(assemblies)
                .AddClasses(c => c.AssignableToAny(assignableTo))
                .AsImplementedInterfaces()
                .WithSingletonLifetime());
            return services;
        }

    }
}