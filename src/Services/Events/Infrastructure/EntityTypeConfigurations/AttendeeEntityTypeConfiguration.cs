using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PingDong.Newmoon.Events.Core;

namespace PingDong.Newmoon.Events.Infrastructure.EntityConfigurations
{
    class AttendeeEntityTypeConfiguration
        : IEntityTypeConfiguration<Attendee>
    {
        public void Configure(EntityTypeBuilder<Attendee> cfg)
        {
            // Tables
            cfg.ToTable("Attendees", EventContext.DefaultSchema);

            // Primary Key
            cfg.HasKey(b => b.Id);

            // Columns
            cfg.Property(b => b.Id)
                .HasColumnName("AttendeeId")
                .ForSqlServerUseSequenceHiLo("attendeseq", EventContext.DefaultSchema);

            cfg.Property(b => b.Identity)
                .HasColumnName("AttendeeIdentity")
                .HasMaxLength(200)
                .IsRequired();

            cfg.Property(b => b.FirstName)
                .HasColumnName("AttendeeFirstName")
                .HasMaxLength(200)
                .IsRequired();

            cfg.Property(b => b.LastName)
                .HasColumnName("AttendeeLastName")
                .HasMaxLength(200)
                .IsRequired();

            // Index
            cfg.HasIndex(b => b.Id)
                .IsUnique()
                .HasName("Events_Attendee_Identity");

            // Ignore
            cfg.Ignore(b => b.DomainEvents);
        }
    }
}
