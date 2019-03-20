using System;
using System.Collections.Generic;
using PingDong.DomainDriven.Core;

namespace PingDong.Newmoon.Events.Core
{
    // Dev Note:
    //    Two important characteristics for value objects:
    //      Without Identity
    //      Immutable    


    // TODO: Value Object
    // EF Core, current 2.1, doesn't support inject value object
    // https://github.com/aspnet/EntityFrameworkCore/issues/12078
    // https://github.com/aspnet/EntityFrameworkCore/issues/9148
    public class Address : ValueObject
    {
        public String No { get; }
        public String Street { get; }
        public String City { get; }
        public String State { get; }
        public String Country { get; }
        public String ZipCode { get; }
        
        public Address(string no, string street, string city, string state, string country, string zipCode)
        {
            No = no;
            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time
            yield return No;
            yield return Street;
            yield return City;
            yield return State;
            yield return Country;
            yield return ZipCode;
        }

        public bool IsValid => !(
                                 string.IsNullOrWhiteSpace(No) || 
                                 string.IsNullOrWhiteSpace(Street) ||
                                 string.IsNullOrWhiteSpace(City)
                                );
    }
}
