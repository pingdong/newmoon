
using AutoMapper;
using PingDong.Newmoon.Events.Core;

namespace PingDong.Newmoon.Events.Service.Models
{
    public class PlaceProfile : Profile
    {
        public PlaceProfile()
        {
            CreateMap<PlaceDTO, Place>();
        }
    }
}
