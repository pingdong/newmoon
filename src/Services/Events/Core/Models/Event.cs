using System;
using System.Collections.Generic;
using System.Linq;
using PingDong.DomainDriven.Core;
using PingDong.Newmoon.Events.Core.Events;
using PingDong.Newmoon.Events.Core.Exceptions;

namespace PingDong.Newmoon.Events.Core
{
    public class Event : Entity, IAggregateRoot
    {
        #region ctor
        public Event(string name, DateTime startTime, DateTime endTime)
        {
            Name = name;
            StartTime = startTime;
            EndTime = endTime;
        }
        #endregion

        #region Mapped fields
        // Support from EF 1.1, database column could map to field
        private DateTime _createTime = DateTime.UtcNow;
        #endregion

        #region properties
        public string Name { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public void UpdateDetail(string name, DateTime startTime, DateTime endTime)
        {
            this.Name = name;
            this.StartTime = startTime;
            this.EndTime = endTime;

            AddDomainEvent(new EventUpdatedDomainEvent(this));
        }
        #endregion

        #region Attendees
        // cannot be made readonly as EF need set it
        private readonly List<Attendee> _attendees = new List<Attendee>();
        public IReadOnlyCollection<Attendee> Attendees => _attendees.AsReadOnly();

        public void AddAttendee(Attendee attendee)
        {
            // check attendee here to guarantee its validation
            //    And all validation should be done in front of modifing data
            //    to avoid partial changing.
            if (attendee == null)
                throw new ArgumentNullException(nameof(attendee));

            var existingAttendee = _attendees.FirstOrDefault(o => o.Identity == attendee.Identity);

            if (existingAttendee != null)
                throw new ArgumentException($"Attendee: {attendee.Identity} is already added");
            
            // Mutate internal data
            this._attendees.Add(attendee);

            AddDomainEvent(new AttendeeAddedDomainEvent(attendee));
        }

        public void RemoveAttendee(string identity)
        {
            if (string.IsNullOrWhiteSpace(identity))
                throw new ArgumentNullException(nameof(identity));

            var existingAttendee = _attendees.FirstOrDefault(o => o.Identity.Equals(identity));
            
            if (existingAttendee == null)
                throw new ArgumentOutOfRangeException($"Attendee: {identity} isn't existed");

            this._attendees.Remove(existingAttendee);
            AddDomainEvent(new AttendeeRemovedDomainEvent(existingAttendee));
        }
        #endregion

        #region Place
        // Outside the Event Aggreate
        // Using PK to disable diect navigation to guarantee the consistency
        public int? PlaceId => _placeId;
        private int? _placeId;

        public void ChangePlace(int placeId)
        {
            if (placeId < 1)
                throw new ArgumentException(nameof(placeId));

            this._placeId = placeId;

            AddDomainEvent(new PlaceBookedDomainEvent(placeId));
        }
        #endregion

        #region Status
        private int _statusId = EventStatus.Created.Id;
        // Keep "Original" value
        //   if return from EF, it has last saved value
        //   So cannot remove 'private set;'
        public EventStatus Status { get; private set; } = EventStatus.Created;

        public void Confirm()
        {
            if (_statusId != EventStatus.Created.Id)
                throw new EventDomainException("Can only confirm an unconfirmed event");

            AddDomainEvent(new EventConfirmedDomainEvent(this));
            this._statusId = EventStatus.Confirmed.Id;
        }

        public void Start()
        {
            if (_statusId != EventStatus.Confirmed.Id)
                throw new EventDomainException("Can only start a confirmed event");

            AddDomainEvent(new EventStartedDomainEvent(this));
            this._statusId = EventStatus.Ongoing.Id;
        }

        public void Finish()
        {
            if (_statusId != EventStatus.Ongoing.Id)
                throw new EventDomainException("Can only end an ongoing event");

            AddDomainEvent(new EventEndedDomainEvent(this));
            this._statusId = EventStatus.Ended.Id;
        }

        public void Cancel()
        {
            if (_statusId != EventStatus.Created.Id && _statusId != EventStatus.Confirmed.Id)
                throw new EventDomainException($"Cannot Cancel a event from {this.Status.Name}.");

            AddDomainEvent(new EventCancelledDomainEvent(this));
            this._statusId = EventStatus.Cancelled.Id;
        }
        #endregion
    }
}
