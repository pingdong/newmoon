namespace PingDong.Newmoon.Events.Service.Models
{
    public class AddressDTO
    {
        public string No { get; }
        public string Street { get; }
        public string City { get; }
        public string State { get; }
        public string Country { get; }
        public string ZipCode { get; }

        public AddressDTO(string no, string street, string city, string state, string country, string zipcode)
        {
            No = no;
            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipcode;
        }
    }

    #region TODO: Value Object
    //public class AddressProfile : Profile
    //{
    //    public AddressProfile()
    //    {
    //        CreateMap<AddressDTO, Address>();
    //    }
    //}
    #endregion
}
