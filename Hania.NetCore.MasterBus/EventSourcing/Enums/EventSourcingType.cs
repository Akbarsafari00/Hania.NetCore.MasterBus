namespace Hania.NetCore.MasterBus.EventSourcing.Enums
{
    public class EventSourcingType
    {
        private EventSourcingType(string value) { Value = value; }
        public string Value { get; private set; }
        
        public static EventSourcingType Mongo   { get { return new EventSourcingType("Mongo"); } }
        public static EventSourcingType EfCore   { get { return new EventSourcingType("EfCore"); } }
    }
}