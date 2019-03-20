﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PingDong.Newmoon.Events.Core;

namespace PingDong.Newmoon.Events.Infrastructure.Migrations.EntityConfigurations
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
                .ForSqlServerUseSequenceHiLo("EventSeq", EventContext.DefaultSchema);

            cfg.Property(b => b.Name)
                .HasColumnName("EventName")
                .IsRequired();

            cfg.Property(b => b.StartTime)
                .IsRequired();

            cfg.Property(b => b.EndTime)
                .IsRequired();

            // Back field
            cfg.Property("_createTime")
                .HasColumnName("CreatedTime")
                .IsRequired();

            cfg.Property("_statusId")
                .HasColumnName("StatusId")
                .IsRequired();

            cfg.Property(b => b.PlaceId)
                .HasColumnName("PlaceId")
                .IsRequired(false);

            // Child Entities
            cfg.HasMany(b => b.Attendees)
                .WithOne()
                .HasForeignKey("EventId")
                .OnDelete(DeleteBehavior.Cascade);
            cfg.Metadata.FindNavigation(nameof(Event.Attendees))
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            // Ignore
            cfg.Ignore(b => b.Status);
            cfg.Ignore(b => b.DomainEvents);
        }
    }
}