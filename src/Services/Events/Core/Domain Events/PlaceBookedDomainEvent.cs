using MediatR;

namespace PingDong.Newmoon.Events.Core.Events
{
    public class PlaceBookedDomainEvent : INotification
    {
        public int PlaceId { get; }

        public PlaceBookedDomainEvent(int placeId)
        {
            PlaceId = placeId;
        }
    }
}
