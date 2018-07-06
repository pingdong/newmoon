using System;
using PingDong.DomainDriven.Core;
using PingDong.Newmoon.Events.Core.Events;
using PingDong.Newmoon.Events.Core.Exceptions;

namespace PingDong.Newmoon.Events.Core
{

    public class Place : Entity, IAggregateRoot
    {
        #region ctor

        public Place(string name, string no, string street, string city, string state, string country, string zipCode)
        {
            Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name));

            No = no;
            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;
        }

        #endregion

        #region Properties
        public string No { get; private set; }
        public string Street { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }

        public string Name { get; private set; }

        public void Update(string name, string no, string street, string city, string state, string country, string zipCode)
        {
            Name = name;
            No = no;
            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;

            AddDomainEvent(new PlaceUpdatedDomainEvent(this));
        }

        #endregion

        #region Occupied

        public bool IsOccupied { get; private set; }

        public void Engage()
        {
            if (IsOccupied)
                throw new EventDomainException($"{Name} is occupied");

            IsOccupied = true;
        }

        public void Disengage()
        {
            if (!IsOccupied)
                throw new EventDomainException($"{Name} is unoccupied");

            IsOccupied = false;
        }

        #endregion
    }

    #region TODO: Value Object
    //public class Place : Entity, IAggregateRoot
    //{
    //    #region ctor

    //    public Place(string name, Address address)
    //    {
    //        Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name));
    //        Address = (address != null && address.IsValid) ? address : throw new ArgumentNullException(nameof(address));
    //    }

    //    #endregion

    //    #region Properties

    //    public string Name { get; private set; }

    //    public Address Address { get; private set; }

    //    public void Update(string name, Address address)
    //    {
    //        this.Name = name;
    //        this.Address = address;

    //        AddDomainEvent(new PlaceUpdatedDomainEvent(this));
    //    }

    //    #endregion

    //    #region Occupied

    //    public bool IsOccupied { get; private set; }

    //    public void Engage()
    //    {
    //        if (IsOccupied)
    //            throw new EventDomainException($"{Name} is occupied");

    //        IsOccupied = true;
    //    }

    //    public void Disengage()
    //    {
    //        if (!IsOccupied)
    //            throw new EventDomainException($"{Name} is unoccupied");

    //        IsOccupied = false;
    //    }

    //    #endregion
    //}
    #endregion
}
