namespace MBrokerDo
{
    public class PoliceHQ
    {
        PoliceChief _Chief = null;
        PolicePatrol[] _Patrols = null;

        public void DirectCall(HandlerType handlerType)
        {
            MsgHandlerBase handler = null;
            MsgBase m = null;

            switch (handlerType)
            {
                case HandlerType.Chief:
                    handler = new PoliceChief("MacMahon");
                    m = new ChiefMsg();
                    break;

                case HandlerType.Patrol:
                    handler = new PolicePatrol("Car102");
                    m = new PatrolMsg();
                    break;
            }

            Clock.Timer.Restart();
            handler.ReferencePoint(m);

            // clean up instance
            handler.UnregisterHandlers();
            handler = null;
        }

        public void SendMsg(MsgBase m)
        {
            switch (m.MessageType)
            {
                case MsgType.Chief:
                    CheckCreateChiefInstance();
                    break;

                case MsgType.Patrol:
                    CheckCreatePatrolInstances();
                    break;

                case MsgType.Broadcast:
                    CheckCreateChiefInstance();
                    CheckCreatePatrolInstances();
                    break;
            }
            
            Clock.Timer.Restart();
            Broker.PoliceDispatcher.Send(m);
        }

        public void SendCallbackableMsgToOnePatrolCar(HiSpeedPursuitMsg m)
        {
            Clock.Timer.Restart();
            m.Callback = CallbackFromCar209AboutHiSpeedPursuit;
            Broker.PoliceDispatcher.Send(m);
        }

        public void CallbackFromCar209AboutHiSpeedPursuit(string msg)
        {
            Clock.Timer.Stop();
            MsgHandlerBase.TraceLog(MsgType.Callback, SenderType.Patrol, HandlerType.PoliceHQ, "Dispatcher");
        }
        
        void CheckCreateChiefInstance()
        {
            if (_Chief == null)
                _Chief = new PoliceChief("MacMahon", true);
        }

        void CheckCreatePatrolInstances()
        {
            if (_Patrols == null)
                _Patrols = new PolicePatrol[2];

            if(_Patrols[0] == null)
                _Patrols[0] = new PolicePatrol("Car102", true);

            if (_Patrols[1] == null)
                _Patrols[1] = new PolicePatrol("Car209", true);
        }
    }
}