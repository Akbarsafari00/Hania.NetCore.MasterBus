// ReSharper disable All
namespace Hania.NetCore.MasterBus.Messaging.Enums
{
    public class MessageType
    {
        private MessageType(string value) { Value = value; }
        public string Value { get; private set; }
        
        public static MessageType Event   { get { return new MessageType("Event"); } }
        public static MessageType Command   { get { return new MessageType("Command"); } }
    }
}