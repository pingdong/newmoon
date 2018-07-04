using System;

namespace PingDong.Newmoon.Events.Core.Exceptions
{
    /// <summary>
    /// Exception type for domain exceptions
    /// </summary>
    public class EventDomainException : Exception
    {
        public EventDomainException() : base()
        { }

        public EventDomainException(string message)
            : base(message)
        { }

        public EventDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
