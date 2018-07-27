using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PingDong.EventBus.Services
{
    public class EventBusLogServiceDbContext : DbContext
    {       
        public EventBusLogServiceDbContext(DbContextOptions<EventBusLogServiceDbContext> options) : base(options)
        {
        }

        public DbSet<EventLogEntity> IntegrationEventLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {          
            builder.Entity<EventLogEntity>(ConfigureIntegrationEventLogEntity);
        }

        void ConfigureIntegrationEventLogEntity(EntityTypeBuilder<EventLogEntity> builder)
        {
            builder.ToTable("logs", "eventbus");

            builder.HasKey(e => e.EventId);

            builder.Property(e => e.EventId)
                .IsRequired();

            builder.Property(e => e.Content)
                .IsRequired();

            builder.Property(e => e.CreationTime)
                .IsRequired();

            builder.Property(e => e.State)
                .IsRequired();

            builder.Property(e => e.TimesSent)
                .IsRequired();

            builder.Property(e => e.EventTypeName)
                .IsRequired();

        }
    }
}
