using System;

namespace MBrokerDo
{
    public static class Extensions
    {
        public static int ToMicroseconds(this TimeSpan ts)
        {
            return (int)(ts.TotalMilliseconds * 1000.0d);
        }
    }
}
