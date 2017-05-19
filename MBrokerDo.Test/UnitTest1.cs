using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace MBrokerDo.Test
{
    [TestClass]
    public class DirectCallVSMessageBroker
    {
        [TestMethod]
        public void SendAndReceivePerformance()
        {
            var hq = new PoliceHQ();

            Trace.WriteLine("\n## Beginning DirectCall vs MessageBroker performance test ##");
            Trace.WriteLine("\n[Direct reference chief two times]");
            hq.DirectCall(HandlerType.Chief);  // 0 ms
            hq.DirectCall(HandlerType.Chief);  // 0 ms
 
            Trace.WriteLine("\n[Direct reference two patrol cars twice]");
            hq.DirectCall(HandlerType.Patrol); // 0 ms
            hq.DirectCall(HandlerType.Patrol); // 0 ms
 
            Trace.WriteLine("\n[Send ChiefMsg to chief two times]");
            hq.SendMsg(new ChiefMsg()); // 8 ms
            hq.SendMsg(new ChiefMsg()); // 0 ms
 
            Trace.WriteLine("\n[Send PatrolMsg to two patrol cars twice]");
            hq.SendMsg(new PatrolMsg()); // 0 ms
            hq.SendMsg(new PatrolMsg()); // 0 ms

            Trace.WriteLine("\n[Send HiSpeedPursuitMsg (callbackable) to one patrol twice]");
            hq.SendCallbackableMsgToOnePatrolCar(new HiSpeedPursuitMsg("Car209")); // 0 ms
            hq.SendCallbackableMsgToOnePatrolCar(new HiSpeedPursuitMsg("Car209")); // 0 ms

            Trace.WriteLine("\n[Send BroadcastMsg message twice]");
            hq.SendMsg(new BroadcastMsg()); // chief 0 ms, patrol 0 ms
            hq.SendMsg(new BroadcastMsg()); // chief 0 ms, patrol 0 ms

            Trace.WriteLine("\nTest ended.\n");

            Assert.IsNotNull(hq);
        }
      
        [TestMethod]
        public void TimeSpanExtension()
        {
            var ts = new TimeSpan(512 * 10);
            Assert.AreEqual(0.512, ts.TotalMilliseconds);
            Assert.AreEqual(512, ts.ToMicroseconds());
        }

        private TestContext testContextInstance;
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
    }
}