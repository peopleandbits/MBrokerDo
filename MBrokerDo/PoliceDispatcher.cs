using MemBroker;

namespace MBrokerDo
{
    public static class Broker
    {
        static Broker()
        {
            PoliceDispatcher = new MessageBroker();
        }

        public static MessageBroker PoliceDispatcher { get; set; }
    }
}
