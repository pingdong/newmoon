using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PingDong.Newmoon.Events.Core.Events;
using PingDong.Newmoon.Events.Core.Exceptions;
using PingDong.QualityTools.Core;
using PingDong.QualityTools.DomainDriven.Core;

namespace PingDong.Newmoon.Events.Core.Test
{
    [TestClass]
    public class EventTest
    {
        #region Status
        #region Init
        [TestMethod]
        public void Status_AfterCreated()
        {
            var evt = CreateDefaultEvent();
            Assert.AreEqual(evt.GetPrivateFieldValue<int>("_statusId"), EventStatus.Created.Id);
        }
        #endregion

        #region Confirm
        [TestMethod]
        public void Status_Confirm()
        {
            var evt = CreateDefaultEvent();
            Assert.AreEqual(evt.GetPrivateFieldValue<int>("_statusId"), EventStatus.Created.Id);
            evt.Confirm();
            Assert.AreEqual(evt.GetPrivateFieldValue<int>("_statusId"), EventStatus.Confirmed.Id);
        }
        [TestMethod]
        [ExpectedException(typeof(EventDomainException))]
        public void Status_Confirm_Confirmed()
        {
            var evt = CreateDefaultEvent();
            evt.Confirm();
            Assert.AreEqual(evt.GetPrivateFieldValue<int>("_statusId"), EventStatus.Confirmed.Id);
            evt.Confirm();
        }
        [TestMethod]
        [ExpectedException(typeof(EventDomainException))]
        public void Status_Confirm_Started()
        {
            var evt = CreateDefaultEvent();
            evt.Confirm();
            evt.Start();
            Assert.AreEqual(evt.GetPrivateFieldValue<int>("_statusId"), EventStatus.Ongoing.Id);
            evt.Confirm();
        }
        [TestMethod]
        [ExpectedException(typeof(EventDomainException))]
        public void Status_Confirm_Ended()
        {
            var evt = CreateDefaultEvent();
            evt.Confirm();
            evt.Start();
            evt.Finish();
            Assert.AreEqual(evt.GetPrivateFieldValue<int>("_statusId"), EventStatus.Ended.Id);
            evt.Confirm();
        }
        [TestMethod]
        [ExpectedException(typeof(EventDomainException))]
        public void Status_Confirm_Cancelled()
        {
            var evt = CreateDefaultEvent();
            Assert.AreEqual(evt.GetPrivateFieldValue<int>("_statusId"), EventStatus.Created.Id);
            evt.Confirm();
            evt.Cancel();
            Assert.AreEqual(evt.GetPrivateFieldValue<int>("_statusId"), EventStatus.Cancelled.Id);
            evt.Confirm();
        }
        #endregion

        #region Start
        [TestMethod]
        [ExpectedException(typeof(EventDomainException))]
        public void Status_Start_Unconfirmed()
        {
            var evt = CreateDefaultEvent();
            Assert.AreEqual(evt.GetPrivateFieldValue<int>("_statusId"), EventStatus.Created.Id);
            evt.Start();
        }
        [TestMethod]
        public void Status_Start()
        {
            var evt = CreateDefaultEvent();
            evt.Confirm();
            Assert.AreEqual(evt.GetPrivateFieldValue<int>("_statusId"), EventStatus.Confirmed.Id);
            evt.Start();
            Assert.AreEqual(evt.GetPrivateFieldValue<int>("_statusId"), EventStatus.Ongoing.Id);
        }
        [TestMethod]
        [ExpectedException(typeof(EventDomainException))]
        public void Status_Start_Started()
        {
            var evt = CreateDefaultEvent();
            evt.Confirm();
            evt.Start();
            Assert.AreEqual(evt.GetPrivateFieldValue<int>("_statusId"), EventStatus.Ongoing.Id);
            evt.Start();
        }
        [TestMethod]
        [ExpectedException(typeof(EventDomainException))]
        public void Status_Start_Ended()
        {
            var evt = CreateDefaultEvent();
            evt.Confirm();
            evt.Start();
            evt.Finish();
            Assert.AreEqual(evt.GetPrivateFieldValue<int>("_statusId"), EventStatus.Ended.Id);
            evt.Start();
        }
        [TestMethod]
        [ExpectedException(typeof(EventDomainException))]
        public void Status_Start_Cancelled()
        {
            var evt = CreateDefaultEvent();
            evt.Confirm();
            evt.Cancel();
            Assert.AreEqual(evt.GetPrivateFieldValue<int>("_statusId"), EventStatus.Cancelled.Id);
            evt.Start();
        }
        #endregion

        #region End
        [TestMethod]
        [ExpectedException(typeof(EventDomainException))]
        public void Status_End_Unconfirmed()
        {
            var evt = CreateDefaultEvent();
            Assert.AreEqual(evt.GetPrivateFieldValue<int>("_statusId"), EventStatus.Created.Id);
            evt.Finish();
        }
        [TestMethod]
        [ExpectedException(typeof(EventDomainException))]
        public void Status_End_Confirmed()
        {
            var evt = CreateDefaultEvent();
            evt.Confirm();
            Assert.AreEqual(evt.GetPrivateFieldValue<int>("_statusId"), EventStatus.Confirmed.Id);
            evt.Finish();
        }
        [TestMethod]
        public void Status_End()
        {
            var evt = CreateDefaultEvent();
            evt.Confirm();
            evt.Start();
            Assert.AreEqual(evt.GetPrivateFieldValue<int>("_statusId"), EventStatus.Ongoing.Id);
            evt.Finish();
            Assert.AreEqual(evt.GetPrivateFieldValue<int>("_statusId"), EventStatus.Ended.Id);
        }
        [TestMethod]
        [ExpectedException(typeof(EventDomainException))]
        public void Status_End_Ended()
        {
            var evt = CreateDefaultEvent();
            evt.Confirm();
            evt.Start();
            evt.Finish();
            Assert.AreEqual(evt.GetPrivateFieldValue<int>("_statusId"), EventStatus.Ended.Id);
            evt.Finish();
        }
        [TestMethod]
        [ExpectedException(typeof(EventDomainException))]
        public void Status_End_Cancelled()
        {
            var evt = CreateDefaultEvent();
            evt.Cancel();
            Assert.AreEqual(evt.GetPrivateFieldValue<int>("_statusId"), EventStatus.Cancelled.Id);
            evt.Finish();
        }
        #endregion

        #region Cancel
        [TestMethod]
        public void Status_Cancel_Unconfirmed()
        {
            var evt = CreateDefaultEvent();
            Assert.AreEqual(evt.GetPrivateFieldValue<int>("_statusId"), EventStatus.Created.Id);
            evt.Cancel();
            Assert.AreEqual(evt.GetPrivateFieldValue<int>("_statusId"), EventStatus.Cancelled.Id);
        }

        [TestMethod]
        public void Status_Cancel_Confirmed()
        {
            var evt = CreateDefaultEvent();
            evt.Confirm();
            Assert.AreEqual(evt.GetPrivateFieldValue<int>("_statusId"), EventStatus.Confirmed.Id);
            evt.Cancel();
            Assert.AreEqual(evt.GetPrivateFieldValue<int>("_statusId"), EventStatus.Cancelled.Id);
        }
        [TestMethod]
        [ExpectedException(typeof(EventDomainException))]
        public void Status_Cancel_Started()
        {
            var evt = CreateDefaultEvent();
            evt.Confirm();
            evt.Start();
            Assert.AreEqual(evt.GetPrivateFieldValue<int>("_statusId"), EventStatus.Ongoing.Id);
            evt.Cancel();
        }
        [TestMethod]
        [ExpectedException(typeof(EventDomainException))]
        public void Status_Cancel_Ended()
        {
            var evt = CreateDefaultEvent();
            evt.Confirm();
            evt.Start();
            evt.Finish();
            Assert.AreEqual(evt.GetPrivateFieldValue<int>("_statusId"), EventStatus.Ended.Id);
            evt.Cancel();
        }
        [TestMethod]
        [ExpectedException(typeof(EventDomainException))]
        public void Status_Cancel_Cancelled()
        {
            var evt = CreateDefaultEvent();
            evt.Cancel();
            Assert.AreEqual(evt.GetPrivateFieldValue<int>("_statusId"), EventStatus.Cancelled.Id);
            evt.Cancel();
        }
        #endregion

        #region Domain Event
        [TestMethod]
        public void Status_DomainEvent_Create_End()
        {
            var evt = CreateDefaultEvent();

            evt.Confirm();
            Assert.IsTrue(evt.HasDomainEvent(typeof(EventConfirmedDomainEvent)));

            evt.Start();
            Assert.IsTrue(evt.HasDomainEvent(typeof(EventStartedDomainEvent)));

            evt.Finish();
            Assert.IsTrue(evt.HasDomainEvent(typeof(EventEndedDomainEvent)));
        }

        [TestMethod]
        public void Status_DomainEvent_Confirm_Cancel()
        {
            var evt = CreateDefaultEvent();

            evt.Cancel();
            Assert.IsTrue(evt.HasDomainEvent(typeof(EventCancelledDomainEvent)));
        }

        [TestMethod]
        public void Status_DomainEvent_Create_Cancel()
        {
            var evt = CreateDefaultEvent();

            evt.Confirm();
            Assert.IsTrue(evt.HasDomainEvent(typeof(EventConfirmedDomainEvent)));

            evt.Cancel();
            Assert.IsTrue(evt.HasDomainEvent(typeof(EventCancelledDomainEvent)));
        }
        #endregion
        #endregion

        #region Attendees
        [TestMethod]
        public void Properties_Attendee_Add()
        {
            var evt = CreateDefaultEvent();

            var attendee = new Attendee(Guid.NewGuid().ToString(), "F", "L");
            evt.AddAttendee(attendee);

            Assert.IsNotNull(evt.Attendees.FirstOrDefault(a => a.Identity == attendee.Identity));
            Assert.IsTrue(evt.HasDomainEvent(typeof(AttendeeAddedDomainEvent)));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Properties_Attendee_Add_Duplicated()
        {
            var evt = CreateDefaultEvent();

            var attendee = new Attendee(Guid.NewGuid().ToString(), "F", "L");
            evt.AddAttendee(attendee);
            evt.AddAttendee(attendee);
        }

        [TestMethod]
        public void Properties_Attendee_Remove()
        {
            var evt = CreateDefaultEvent();
            var attendee = new Attendee(Guid.NewGuid().ToString(), "F", "L");
            evt.AddAttendee(attendee);

            evt.RemoveAttendee(attendee.Identity);

            Assert.IsNull(evt.Attendees.FirstOrDefault(a => a.Identity == attendee.Identity));
            Assert.IsTrue(evt.HasDomainEvent(typeof(AttendeeRemovedDomainEvent)));

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]

        public void Properties_Attendee_Remove_NotExisted()
        {
            var evt = CreateDefaultEvent();
            evt.RemoveAttendee(Guid.NewGuid().ToString());

        }
        #endregion

        #region Place
        [TestMethod]
        public void Properties_Place()
        {
            var placeId = 11;
            var evt = CreateDefaultEvent();
            evt.ChangePlace(11);

            Assert.AreEqual(placeId, evt.PlaceId);
            Assert.IsTrue(evt.HasDomainEvent(typeof(PlaceBookedDomainEvent)));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Properties_Place_InvalidPlaceId()
        {
            var evt = CreateDefaultEvent();
            evt.ChangePlace(0);
        }
        #endregion

        #region Properties
        [TestMethod]
        public void Properties()
        {
            var name = "Test";
            var startTime = DateTime.Now;
            var endTime = DateTime.Now.AddHours(3);

            var evt = new Event(name, startTime, endTime);

            Assert.AreEqual(name, evt.Name);
            Assert.AreEqual(startTime, evt.StartTime);
            Assert.AreEqual(endTime, evt.EndTime);
        }

        [TestMethod]
        public void Properties_PrivateField()
        {
            var now = DateTime.UtcNow;
            var evt = CreateDefaultEvent();
            var evtCreateTime = evt.GetPrivateFieldValue<DateTime>("_createTime");
            var diff = evtCreateTime - now;

            Assert.IsTrue(diff.TotalSeconds < 1);

        }
        [TestMethod]
        public void Properties_Update()
        {
            var name = Guid.NewGuid().ToString();
            var startTime = DateTime.Now;
            var endTime = DateTime.Now.AddHours(3);

            var evt = CreateDefaultEvent();

            Assert.AreNotEqual(name, evt.Name);
            Assert.AreNotEqual(startTime, evt.StartTime);
            Assert.AreNotEqual(endTime, evt.EndTime);

            evt.UpdateDetail(name, startTime, endTime);

            Assert.AreEqual(name, evt.Name);
            Assert.AreEqual(startTime, evt.StartTime);
            Assert.AreEqual(endTime, evt.EndTime);
            Assert.IsTrue(evt.HasDomainEvent(typeof(EventUpdatedDomainEvent)));
        }
        #endregion

        #region Helper

        private Event CreateDefaultEvent()
        {
            return new Event("Test", DateTime.Now, DateTime.Now.AddHours(2));
        }
        #endregion
    }
}
