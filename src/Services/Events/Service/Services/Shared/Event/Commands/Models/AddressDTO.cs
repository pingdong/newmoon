namespace PingDong.Newmoon.Events.Service.Models
{
    public class AddressDTO
    {
        public string No { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
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
