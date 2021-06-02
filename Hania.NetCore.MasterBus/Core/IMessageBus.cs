using System;
using System.Threading.Tasks;
using Hania.NetCore.MasterBus.Core.Commands;
using Hania.NetCore.MasterBus.Core.Events;
using Hania.NetCore.MasterBus.Core.Messaging;

namespace Hania.NetCore.MasterBus.Core
{
    public interface IMessageBus
    {
     
        Task Publish<TEvent>(TEvent @event) where TEvent : IEvent;
        Task Subscribe<TEvent>() where TEvent : IEvent;
        Task Subscribe(Type type);
        
        
        
  
        Task SendTo<TCommand>(string destinationService, string commandName, TCommand commandData) where TCommand : ICommand;
        Task Receive<TCommand>() where TCommand : ICommand;
        Task Receive(Type type);
        
        
        Task SendMessagePacket(MessagePacket packet);
    }
}