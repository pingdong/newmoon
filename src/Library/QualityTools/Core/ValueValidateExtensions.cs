using System;

namespace PingDong.QualityTools.Core
{
    public static class ValueValidateExtensions
    {
        public static bool Equal(this DateTime target, DateTime destination)
        {
            var difference = target - destination;
            return 1 >= difference.TotalSeconds;
        }

        public static bool Equal(this DateTime target, DateTime destination, TimeSpan offset)
        {
            var difference = target - destination;
            return offset >= difference;
        }
    }
}
