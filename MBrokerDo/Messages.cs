using System;

namespace MBrokerDo
{
    public class MsgBase
    {
        public MsgBase(MsgType mtype)
        {
            MessageType = mtype;
        }

        public SenderType Sender { get; set; } = SenderType.PoliceHQ;
        public MsgType MessageType { get; set; }
    }

    public enum MsgType { Chief, Patrol, HiSpeedPursuit, Broadcast, Callback }

    public class ChiefMsg : MsgBase
    {
        public ChiefMsg() : base(MsgType.Chief) { }
    }

    public class PatrolMsg : MsgBase
    {
        public PatrolMsg() : base(MsgType.Patrol) { }
    }

    public class HiSpeedPursuitMsg : MsgBase
    {
        public HiSpeedPursuitMsg(string assignedPatrolName, Action<string> callback = null) : base(MsgType.HiSpeedPursuit)
        {
            AssignedPatrolName = assignedPatrolName;
            Callback = callback;
        }

        public string AssignedPatrolName { get; private set; }
        public Action<string> Callback { get; set; }
    }

    public class BroadcastMsg : MsgBase
    {
        public BroadcastMsg() : base(MsgType.Broadcast) { }
    }
}
