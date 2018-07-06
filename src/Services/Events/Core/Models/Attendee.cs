using System;
using PingDong.DomainDriven.Core;

namespace PingDong.Newmoon.Events.Core
{
    public class Attendee : Entity
    {
        #region ctor
        public Attendee(string identity, string firstName, string lastName)
        {
            Identity = !string.IsNullOrWhiteSpace(identity) ? identity : throw new ArgumentNullException(nameof(identity));
            FirstName = !string.IsNullOrWhiteSpace(firstName) ? firstName : throw new ArgumentNullException(nameof(firstName));
            LastName = !string.IsNullOrWhiteSpace(lastName) ? lastName : throw new ArgumentNullException(nameof(lastName));
        }
        #endregion

        #region Properties
        public string Identity { get; }

        public string FirstName { get; }

        public string LastName { get; }
        #endregion
    }
}
