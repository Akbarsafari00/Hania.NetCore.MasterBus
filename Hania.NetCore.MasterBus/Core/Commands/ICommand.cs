using System;

namespace Hania.NetCore.MasterBus.Core.Commands
{
    public interface ICommand
    {
         Guid CorrelationId { get; set; }
    }
    public interface ICommand<TData>
    { Guid CorrelationId { get; set; }
    }
}