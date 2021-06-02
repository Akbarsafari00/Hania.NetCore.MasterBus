using System;
using System.Threading.Tasks;

namespace Hania.NetCore.MasterBus.Core.Commands
{
    public interface ICommandBus
    {
        Task SendTo<TCommand>(string destinationService, string commandName, TCommand commandData) where TCommand : ICommand;
        
        
        Task Receive<TCommand>() where TCommand : ICommand;
        Task Receive(Type type);
    }
}