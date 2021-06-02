using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Hania.NetCore.MasterBus.Core.Events;

namespace Hania.NetCore.MasterBus.Core.Extensions
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetAllImplementedTypes(this Assembly assembly , Type interfaceType)
        {
            return assembly.GetTypes().Where(x =>
                interfaceType.IsAssignableFrom(x) && x.IsClass ||
                interfaceType.IsAssignableFrom(x.BaseType) && x.IsClass);
        }
        public static IEnumerable<Type> GetAllImplementedTypes(this Assembly[] assemblies , Type interfaceType)
        {
            return assemblies.SelectMany(x=>x.GetTypes()).Where(x =>
                interfaceType.IsAssignableFrom(x) && x.IsClass ||
                interfaceType.IsAssignableFrom(x.BaseType) && x.IsClass);
        }
        
        public static IEnumerable<Type>  GetAllGenericImplementedTypes(this Assembly assembly,Type openGenericType )
        {
            var types = assembly.GetTypes();
            return   from x in types
                from z in x.GetInterfaces()
                let y = x.BaseType
                where
                    y != null && y.IsGenericType &&
                    openGenericType.IsAssignableFrom(y.GetGenericTypeDefinition()) ||
                    z.IsGenericType &&
                    openGenericType.IsAssignableFrom(z.GetGenericTypeDefinition())
                select x;
        }
        
        public static IEnumerable<Type>  GetAllGenericImplementedTypes(this Assembly[] assemblies,Type openGenericType )
        {
            var types = assemblies.SelectMany(x=>x.GetTypes());
            return   from x in types
                from z in x.GetInterfaces()
                let y = x.BaseType
                where
                    y != null && y.IsGenericType &&
                    openGenericType.IsAssignableFrom(y.GetGenericTypeDefinition()) ||
                    z.IsGenericType &&
                    openGenericType.IsAssignableFrom(z.GetGenericTypeDefinition())
                select x;
        }
    }
}