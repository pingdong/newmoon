using Microsoft.VisualStudio.TestTools.UnitTesting;
using PingDong.Newmoon.Events.Core.Events;
using PingDong.Newmoon.Events.Core.Exceptions;
using PingDong.QualityTools.DomainDriven.Core;

namespace PingDong.Newmoon.Events.Core
{
    [TestClass]
    public class PlaceTest
    {
        #region Occupied
        #region Init
        [TestMethod]
        public void Status_AfterCreated()
        {
            var place = CreateDefaultPlace();
            Assert.IsFalse(place.IsOccupied);
        }
        #endregion

        #region Engage
        [TestMethod]
        public void Engage()
        {
            var place = CreateDefaultPlace();
            Assert.IsFalse(place.IsOccupied);
            place.Engage();
            Assert.IsTrue(place.IsOccupied);
        }
        [TestMethod]
        [ExpectedException(typeof(EventDomainException))]
        public void Engage_Occupied()
        {
            var place = CreateDefaultPlace();
            place.Engage();
            Assert.IsTrue(place.IsOccupied);
            place.Engage();
        }
        #endregion

        #region Disengage
        [TestMethod]
        public void Disengage()
        {
            var place = CreateDefaultPlace();
            place.Engage();
            Assert.IsTrue(place.IsOccupied);
            place.Disengage();
            Assert.IsFalse(place.IsOccupied);
        }
        [TestMethod]
        [ExpectedException(typeof(EventDomainException))]
        public void Disengage_Unoccupied()
        {
            var place = CreateDefaultPlace();
            Assert.IsFalse(place.IsOccupied);
            place.Disengage();
        }
        #endregion
        #endregion


        #region Properties

        [TestMethod]
        public void Properties()
        {
            var name = "Test";
            var no = "11";
            var street = "Queen";
            var city = "Auckland";
            var state = "Auckland";
            var country = "New Zealand";
            var zipCode = "1026";

            var place = new Place(name, no, street, city, state, country, zipCode);

            Assert.AreEqual(name, place.Name);
            Assert.AreEqual(no, place.No);
            Assert.AreEqual(street, place.Street);
            Assert.AreEqual(city, place.City);
            Assert.AreEqual(state, place.State);
            Assert.AreEqual(country, place.Country);
            Assert.AreEqual(zipCode, place.ZipCode);
            Assert.IsFalse(place.IsOccupied);
        }


        [TestMethod]
        public void Properties_Update()
        {
            var name = "Test";
            var no = "11";
            var street = "Queen";
            var city = "Auckland";
            var state = "Auckland";
            var country = "New Zealand";
            var zipCode = "1026";

            var place = CreateDefaultPlace();
            Assert.AreNotEqual(name, place.Name);
            Assert.AreNotEqual(no, place.No);
            Assert.AreNotEqual(street, place.Street);
            Assert.AreEqual(city, place.City);
            Assert.AreEqual(state, place.State);
            Assert.AreEqual(country, place.Country);
            Assert.AreNotEqual(zipCode, place.ZipCode);

            place.Update(name, no, street, city, state, country, zipCode);
            Assert.AreEqual(name, place.Name);
            Assert.AreEqual(no, place.No);
            Assert.AreEqual(street, place.Street);
            Assert.AreEqual(city, place.City);
            Assert.AreEqual(state, place.State);
            Assert.AreEqual(country, place.Country);
            Assert.AreEqual(zipCode, place.ZipCode);
            Assert.IsFalse(place.IsOccupied);
            Assert.IsTrue(place.HasDomainEvent(typeof(PlaceUpdatedDomainEvent)));
        }
        #endregion

        #region Helper
        private Place CreateDefaultPlace()
        {
            return new Place("Default", "20", "Symond", "Auckland", "Auckland", "New Zealand", "0926");
        }
        #endregion


        #region TODO: Value Object
        //#region Properties

        //[TestMethod]
        //public void Properties()
        //{
        //    var address = new Address("11", "Queen", "Auckland", "Auckland", "New Zealand", "1026");
        //    var name = "Test";

        //    var place = new Place(name, address);

        //    Assert.AreEqual(name, place.Name);
        //    Assert.AreEqual(address, place.Address);
        //    Assert.IsFalse(place.IsOccupied);
        //}


        //[TestMethod]
        //public void Properties_Update()
        //{
        //    var address = new Address("11", "Queen", "Auckland", "Auckland", "New Zealand", "1026");
        //    var name = "Test";

        //    var place = CreateDefaultPlace();
        //    Assert.AreNotEqual(name, place.Name);
        //    Assert.AreNotEqual(address, place.Address);

        //    place.Update(name, address);
        //    Assert.AreEqual(name, place.Name);
        //    Assert.AreEqual(address, place.Address);
        //    Assert.IsFalse(place.IsOccupied);
        //    Assert.IsTrue(place.HasDomainEvent(typeof(PlaceUpdatedDomainEvent)));
        //}
        //#endregion

        //#region Helper
        //private Place CreateDefaultPlace()
        //{
        //    return new Place("Default", new Address("20", "Symond", "Auckland", "Auckland", "New Zealand", "0926"));
        //}
        //#endregion
        #endregion
    }
}
