using MediatR;
using Microsoft.EntityFrameworkCore;
using PingDong.DomainDriven.Core;
using PingDong.DomainDriven.Infrastructure;
using PingDong.Newmoon.Events.Core;
using PingDong.Newmoon.Events.Infrastructure.Migrations.EntityConfigurations;

namespace PingDong.Newmoon.Events.Infrastructure
{
    public class EventContext : DbContextBase
    {
        public const string DefaultSchema = "events";

        // Event
        public DbSet<Event> Events { get; set; }
        public DbSet<Attendee> Attendees { get; set; }

        // Requests
        public DbSet<ClientRequest> Requests { get; set; }

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
            // Place
            modelBuilder.ApplyConfiguration(new PlaceEntityTypeConfiguration());
            // Request Manager
            modelBuilder.ApplyConfiguration(new ClientRequestEntityTypeConfiguration());
        }  
    }
}
