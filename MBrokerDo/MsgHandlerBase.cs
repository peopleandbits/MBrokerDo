using MemBroker;
using System.Diagnostics;

namespace MBrokerDo
{
    public abstract class MsgHandlerBase
    {
        #region Constructors
        public MsgHandlerBase(HandlerType handler, string name, bool messaging)
        {
            HandlerType = handler;
            Name = name;
            IsMessagingEnabled = messaging;
        }
        #endregion

        #region Properties
        public HandlerType HandlerType { get; private set; }
        public string Name { get; set; }
        public bool IsMessagingEnabled { get; private set; }
        #endregion

        #region Direct calling
        public void ReferencePoint(MsgBase m)
        {
            Clock.Timer.Stop();
            TraceLog(m.MessageType, m.Sender, HandlerType, $"{Name}/{GetHashCode()}");
        }
        #endregion

        #region Message registering, handling & sending 
        public virtual void RegisterHandlers()
        {
            if (IsMessagingEnabled)
                Broker.PoliceDispatcher.Register<BroadcastMsg>(this, BroadcastMsgHandler);
        }

        public virtual void UnregisterHandlers()
        {
            if (IsMessagingEnabled)
                Broker.PoliceDispatcher.Unregister<BroadcastMsg>(this, BroadcastMsgHandler);
        }

        public void BroadcastMsgHandler(BroadcastMsg m)
        {
            Clock.Timer.Stop();
            TraceLog(m.MessageType, m.Sender, HandlerType, $"{Name}/{GetHashCode()}");
        }
        #endregion

        #region Helpers
        public static void TraceLog(MsgType mt, SenderType sender, HandlerType handler, string handlerDetails = null)
        {
            string slashName = string.IsNullOrEmpty(handlerDetails) ? string.Empty : $"/{handlerDetails}";
            Trace.WriteLine($"{mt}Msg from {sender} to {handler}{slashName} was received in {Clock.Timer.ElapsedMilliseconds}ms ({Clock.Timer.Elapsed.ToMicroseconds()}μs)");
        }
        #endregion
    }

    public enum SenderType { Chief, Patrol, PoliceHQ }
    public enum HandlerType { Chief, Patrol, PoliceHQ }
}
