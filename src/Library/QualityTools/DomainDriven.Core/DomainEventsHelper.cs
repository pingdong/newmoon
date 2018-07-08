using System;
using System.Collections.Generic;
using System.Linq;
using PingDong.DomainDriven.Core;

namespace PingDong.QualityTools.DomainDriven.Core
{
    public static class DomainEventsHelper
    {
        #region Domain Events

        public static bool HasNoDomainEvents(this Entity entity)
        {
            return entity.DomainEvents == null || entity.DomainEvents.Count == 0;
        }

        public static bool HasDomainEvent(this Entity entity, Type expectedType, int expectedCount = 1)
        {
            var events = entity.DomainEvents.Where(t => t.GetType() == expectedType);

            return events.Count() == expectedCount;
        }

        public static bool HasDomainEvents(this Entity entity, IEnumerable<Type> expectedTypes)
        {
            return expectedTypes.All(eventType => HasDomainEvent(entity, eventType));
        }

        #endregion
    }
}
