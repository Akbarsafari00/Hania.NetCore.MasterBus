namespace Hania.NetCore.MasterBus.EventSourcing.Enums
{
    public class OutboxType
    {
        private OutboxType(string value) { Value = value; }
        public string Value { get; private set; }
        
        public static OutboxType Mongo   { get { return new OutboxType("Mongo"); } }
        public static OutboxType EfCore   { get { return new OutboxType("EfCore"); } }
    }
}