using System;
using System.Threading.Tasks;
using Hania.NetCore.MasterBus.Core.Commands;
using Hania.NetCore.MasterBus.Core.Events;
using Hania.NetCore.MasterBus.Core.Messaging;

namespace Hania.NetCore.MasterBus.Core
{
    public interface IMessageConsumer
    {
    
        Task ConsumeEvent(string sender, MessagePacket packet);
        Task ConsumeCommand(string sender, MessagePacket packet);
    }
}