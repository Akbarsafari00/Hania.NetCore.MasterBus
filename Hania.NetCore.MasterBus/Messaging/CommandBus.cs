using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Hania.NetCore.MasterBus.Core;
using Hania.NetCore.MasterBus.Core.Commands;
using Hania.NetCore.MasterBus.Core.Events;
using Hania.NetCore.MasterBus.Core.EventSource;
using Hania.NetCore.MasterBus.Core.Outbox;
using Hania.NetCore.MasterBus.EventSourcing.Enums;
using Hania.NetCore.MasterBus.Messaging.Enums;
using Hania.NetCore.MasterBus.Outbox;
using Newtonsoft.Json;
using ICommand = Hania.NetCore.MasterBus.Core.Commands.ICommand;

namespace Hania.NetCore.MasterBus.Messaging
{
    public class CommandBus : ICommandBus
    {

        private readonly IOutboxStore _outboxStore; 
        private readonly IMessageBus _messageBus;
        private readonly ICommandDispatcher _commandDispatcher;

        public CommandBus(IOutboxStore outboxStore, IMessageBus messageBus, ICommandDispatcher commandDispatcher)
        {
            _outboxStore = outboxStore;
            _messageBus = messageBus;
            _commandDispatcher = commandDispatcher;
        }


      

        public async Task SendTo<TCommand>(string destinationService, string commandName, TCommand command) where TCommand : ICommand
        {
            if (_outboxStore!=null)
            {
                var payload = JsonConvert.SerializeObject(command);
                var item = new OutboxItem(Guid.NewGuid(), command.CorrelationId,typeof(TCommand),MessageType.Command.Value,payload);
                await _outboxStore.StoreOutbox(item);
            }
            else if (_messageBus!=null)
            {
                await _messageBus.SendTo(destinationService,commandName,command);
            }
            else
            {
                await _commandDispatcher.Send(command);
            }
        }

        public Task Receive<TCommand>() where TCommand : ICommand
        {
            return _messageBus.Receive<TCommand>();
        }

        public Task Receive(Type type)
        {
            return _messageBus.Receive(type);
        }
    }
}