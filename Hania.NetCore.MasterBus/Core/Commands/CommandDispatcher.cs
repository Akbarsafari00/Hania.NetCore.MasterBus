using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hania.NetCore.MasterBus.Core.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Hania.NetCore.MasterBus.Core.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        
        private readonly IServiceProvider _serviceProvider;
        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public async Task Send<TCommand>(TCommand command) where TCommand :  ICommand
        {
            var handlers = _serviceProvider.GetServices<ICommandHandler<TCommand>>();
            foreach (var handler in handlers)
            {
                await handler.Handle(command);
            }
        }

    }
}