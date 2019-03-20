using System;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PingDong.Newmoon.Events.Core.Validations;

namespace PingDong.Newmoon.Events.Core
{
    [TestClass]
    public class EventValidationTest
    {
        #region Name
        [TestMethod]
        public void NameIsEmpty()
        {
            var evt = new Event("", DateTime.Now, DateTime.Now.AddHours(2));

            var rule = new EventValidator();
            rule.ShouldHaveValidationErrorFor(a => a.Name, evt);
        }
        [TestMethod]
        public void NameIsSpace()
        {
            var evt = new Event(" ", DateTime.Now, DateTime.Now.AddHours(2));

            var rule = new EventValidator();
            rule.ShouldHaveValidationErrorFor(a => a.Name, evt);
        }
        [TestMethod]
        public void NameIsNull()
        {
            var evt = new Event(null, DateTime.Now, DateTime.Now.AddHours(2));

            var rule = new EventValidator();
            rule.ShouldHaveValidationErrorFor(a => a.Name, evt);
        }
        #endregion

        #region Time
        [TestMethod]
        public void StartTimeLaterThanEndTime()
        {
            var evt = new Event("Test", DateTime.Now.AddHours(1), DateTime.Now);

            var rule = new EventValidator();
            rule.ShouldHaveValidationErrorFor(a => a.EndTime, evt);
        }
        [TestMethod]
        public void StartTimeEqualsEndTime()
        {
            var time = DateTime.Now;
            var evt = new Event("Test", time, time);

            var rule = new EventValidator();
            rule.ShouldHaveValidationErrorFor(a => a.EndTime, evt);
        }
        [TestMethod]
        public void StartTimeCantBefore9()
        {
            var now = DateTime.Now;
            var evt = new Event("Test", new DateTime(now.Year, now.Month, now.Day, 8, 0, 0), DateTime.Now.AddHours(2));

            var rule = new EventValidator();
            rule.ShouldHaveValidationErrorFor(a => a.StartTime, evt);
        }
        #endregion
    }
}
