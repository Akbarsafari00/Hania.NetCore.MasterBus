using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Hania.NetCore.MasterBus.Core.Events;
using Hania.NetCore.MasterBus.Core.EventSource;
using Hania.NetCore.MasterBus.Outbox;
using Hania.NetCore.MasterBus.Outbox.Stores.Mongo.Document;

namespace Hania.NetCore.MasterBus.Core.Outbox
{
    public interface IOutboxStore
    {
        Task StoreOutbox(OutboxItem item);
        Task StoreInbox(InboxItem item);
        Task SetMessageToProcessed(string id);
        Task<IEnumerable<OutboxDocument<Guid>>> GetAllUnProcessedItem();
        Task<bool> CanReceive(Guid messageId);
       
    }
}