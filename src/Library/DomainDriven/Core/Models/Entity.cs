using System;
using System.Collections.Generic;

using MediatR;

namespace PingDong.DomainDriven.Core
{
    public abstract class Entity
    {
        #region Properties
        public virtual int Id { get; protected set; }
        #endregion

        #region Domain Events
        private List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
        #endregion

        #region Object
        public bool IsTransient()
        {
            return this.Id == default(Int32);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Entity))
                return false;

            if (object.ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            var item = (Entity)obj;

            if (item.IsTransient() || this.IsTransient())
                return false;
            
            return item.Id.Equals(this.Id);
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)
                    _requestedHashCode = this.Id.GetHashCode() ^ 31; 

                return _requestedHashCode.Value;
            }
            
            return base.GetHashCode();
        }
        private int? _requestedHashCode;

        public static bool operator == (Entity left, Entity right)
        {
            if (object.Equals(left, null))
                return object.Equals(right, null) ? true : false;
            
            return left.Equals(right);
        }

        public static bool operator != (Entity left, Entity right)
        {
            return !(left == right);
        }
        #endregion
    }
}
