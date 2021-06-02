using System;
using System.Threading.Tasks;
using Hania.NetCore.MasterBus.Core.Messaging;

namespace Hania.NetCore.MasterBus.Core.Commands
{
    public interface ICommandDispatcher
    {
        Task Send<TCommand>(TCommand command) where TCommand :  ICommand;
    }
}