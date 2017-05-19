namespace MBrokerDo
{
    public class PoliceChief : MsgHandlerBase
    {
        public PoliceChief(string name, bool messaging = false) : base(HandlerType.Chief, name, messaging)
        {
            if(IsMessagingEnabled)
                RegisterHandlers();
        }

        #region Message registering, handling & sending 
        public override void RegisterHandlers()
        {
            Broker.PoliceDispatcher.Register<ChiefMsg>(this, ChiefMsgHandler);
            base.RegisterHandlers();
        }

        public override void UnregisterHandlers()
        {
            Broker.PoliceDispatcher.Unregister(this);
            base.UnregisterHandlers();
        }

        public void ChiefMsgHandler(ChiefMsg m)
        {
            Clock.Timer.Stop();
            TraceLog(m.MessageType, m.Sender, HandlerType, $"{Name}/{GetHashCode()}");
        }
        #endregion
    }
}