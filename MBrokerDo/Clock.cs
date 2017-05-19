using System.Diagnostics;

namespace MBrokerDo
{
    public static class Clock
    {
        static Clock()
        {
            Timer = new Stopwatch();
        }

        public static Stopwatch Timer { get; set; }
    }
}
