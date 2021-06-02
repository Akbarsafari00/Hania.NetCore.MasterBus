using System.Threading.Tasks;

namespace Hania.NetCore.MasterBus.Core.Commands
{
    public interface ICommandHandler<TCommand, TData> where TCommand : ICommand<TData>
    {
        Task  Handle(TCommand request);
    }
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task Handle(TCommand request);
    }
}