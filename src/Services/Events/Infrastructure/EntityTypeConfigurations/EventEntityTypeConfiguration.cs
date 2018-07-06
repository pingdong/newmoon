using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PingDong.Newmoon.Events.Core;

namespace PingDong.Newmoon.Events.Infrastructure.EntityConfigurations
{
    class EventEntityTypeConfiguration
        : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> cfg)
        {
            // Table
            cfg.ToTable("Events", EventContext.DefaultSchema);

            // Primary key
            cfg.HasKey(b => b.Id);

            // Columns
            cfg.Property(b => b.Id)
                .HasColumnName("EventId")
                .ForSqlServerUseSequenceHiLo("buyerseq", EventContext.DefaultSchema);

            cfg.Property(b => b.Name)
                .HasColumnName("EventName")
                .IsRequired();

            cfg.Property(b => b.StartTime)
                .IsRequired();

            cfg.Property(b => b.EndTime)
                .IsRequired();

            // Back field
            cfg.Property("_placeId")
                .HasColumnName("PlaceId")
                .HasColumnType("int")
                .IsRequired(false);

            cfg.Property("_createTime")
                .HasColumnName("CreatedTime")
                .IsRequired();

            // Child Entity
            cfg.Property("_statusId")
                .IsRequired();
            cfg.HasOne(p => p.Status)
                .WithMany()
                .HasForeignKey("StatusId");

            // Child Entities
            cfg.HasMany(b => b.Attendees)
                .WithOne()
                .HasForeignKey("EventId")
                .OnDelete(DeleteBehavior.Cascade);
            cfg.Metadata.FindNavigation(nameof(Event.Attendees))
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            // Ignore
            cfg.Ignore(b => b.DomainEvents);
        }
    }
}
