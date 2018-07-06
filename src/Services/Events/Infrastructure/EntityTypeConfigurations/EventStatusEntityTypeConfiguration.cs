using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PingDong.Newmoon.Events.Core;

namespace PingDong.Newmoon.Events.Infrastructure.EntityConfigurations
{
    class EventStatusEntityTypeConfiguration : IEntityTypeConfiguration<EventStatus>
    {
        public void Configure(EntityTypeBuilder<EventStatus> cfg)
        {
            // Table
            cfg.ToTable("Status", EventContext.DefaultSchema);

            // Primary Key
            cfg.HasKey(o => o.Id);

            // Columns
            cfg.Property(o => o.Id)
                .HasColumnName("StatusId")
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            cfg.Property(o => o.Name)
                .HasColumnName("StatusName")
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
