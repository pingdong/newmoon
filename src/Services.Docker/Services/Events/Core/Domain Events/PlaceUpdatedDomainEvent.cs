using MediatR;

namespace PingDong.Newmoon.Events.Core.Events
{
    public class PlaceUpdatedDomainEvent : INotification
    {
        public Place Place { get; }

        public PlaceUpdatedDomainEvent(Place place)
        {
            Place = place;
        }
    }
}
