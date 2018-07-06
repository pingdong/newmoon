
using AutoMapper;
using PingDong.Newmoon.Events.Core;

namespace PingDong.Newmoon.Events.Service.Models
{
    public class AttendeeDTO
    {
        public string Identity { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }

    public class AttendeeProfile : Profile
    {
        public AttendeeProfile()
        {
            CreateMap<AttendeeDTO, Attendee>();
        }
    }
}
