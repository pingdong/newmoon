using MediatR;
using Microsoft.EntityFrameworkCore;
using PingDong.DomainDriven.Infrastructure;
using PingDong.Newmoon.Events.Core;
using PingDong.Newmoon.Events.Infrastructure.EntityConfigurations;

namespace PingDong.Newmoon.Events.Infrastructure
{
    public class EventContext : DbContextBase
    {
        public const string DefaultSchema = "Events";

        // Event
        public DbSet<Event> Events { get; set; }
        public DbSet<Attendee> Attendees { get; set; }
        public DbSet<EventStatus> EventStatuses { get; set; }

        // Places
        public DbSet<Place> Places { get; set; }

        private EventContext(DbContextOptions<EventContext> options) : base (options) { }

        public EventContext(DbContextOptions<EventContext> options, IMediator mediator) : base(options, mediator)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Event
            modelBuilder.ApplyConfiguration(new EventEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AttendeeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EventStatusEntityTypeConfiguration());
            // Place
            modelBuilder.ApplyConfiguration(new PlaceEntityTypeConfiguration());
            // Request Manager
            modelBuilder.ApplyConfiguration(new ClientRequestEntityTypeConfiguration());
        }  
    }
}
