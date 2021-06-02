using System;
using Hania.NetCore.MasterBus.EventSourcing.Options;
using Hania.NetCore.MasterBus.EventSourcing.Stores.EFCore.Options;

namespace Hania.NetCore.MasterBus.EventSourcing.Stores.EFCore.Builders
{
    public class EfCoreEventSourcingOptionsBuilder
    {
        private readonly EfCoreEventSourcingOptions _option;

        public EfCoreEventSourcingOptionsBuilder()
        {
             _option = new EfCoreEventSourcingOptions();
        }

        public EfCoreEventSourcingOptionsBuilder ConnectionString(string connectionString)
        {

            _option.ConnectionString = connectionString;
            return this;
        }
        
        public EfCoreEventSourcingOptions Build()
        {
            if (_option.ConnectionString==null)
            {
                throw  new Exception($"EFCore EventSourcing Option '{nameof(_option.ConnectionString)}' cannot be null");
            }
            
           
            return _option;
        }
        
    }
}