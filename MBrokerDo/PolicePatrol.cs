namespace MBrokerDo
{
    public class PolicePatrol : MsgHandlerBase
    {
        public PolicePatrol(string name, bool messaging = false) : base(HandlerType.Patrol, name, messaging)
        {
            if(IsMessagingEnabled)
                RegisterHandlers();
        }
        
        #region Message registering, handling & sending 
        public override void RegisterHandlers()
        {
            Broker.PoliceDispatcher.Register<PatrolMsg>(this, PatrolMsgHandler);
            Broker.PoliceDispatcher.Register<HiSpeedPursuitMsg>(this, HiSpeedPursuitMsgHandler);

            base.RegisterHandlers();
        }

        public override void UnregisterHandlers()
        {
            Broker.PoliceDispatcher.Unregister(this);
            base.UnregisterHandlers();
        }

        public void PatrolMsgHandler(PatrolMsg m)
        {
            Clock.Timer.Stop();
            TraceLog(m.MessageType, m.Sender, HandlerType, $"{Name}/{GetHashCode()}");
        }

        public void HiSpeedPursuitMsgHandler(HiSpeedPursuitMsg m)
        {
            if (Name != "Car209")
                return;

            Clock.Timer.Stop();
            TraceLog(m.MessageType, m.Sender, HandlerType, $"{Name}/{GetHashCode()}");
            Clock.Timer.Restart();
            m.Callback("Pursuit ended, suspect in custody.");
        }
        #endregion
    }
}