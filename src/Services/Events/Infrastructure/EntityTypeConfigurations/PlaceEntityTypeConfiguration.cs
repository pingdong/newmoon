using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PingDong.Newmoon.Events.Core;

namespace PingDong.Newmoon.Events.Infrastructure.EntityConfigurations
{
    class PlaceEntityTypeConfiguration
        : IEntityTypeConfiguration<Place>
    {
        public void Configure(EntityTypeBuilder<Place> cfg)
        {
            // Table
            cfg.ToTable("Places", EventContext.DefaultSchema);
            
            // Primary Key
            cfg.HasKey(o => o.Id);

            // Columns
            cfg.Property(o => o.Id)
                .HasColumnName("PlaceId")
                .ForSqlServerUseSequenceHiLo("PlaceSeq", EventContext.DefaultSchema);

            cfg.Property(o => o.Name)
                .HasColumnName("PlaceName")
                .HasColumnType("nvarchar")
                .HasMaxLength(200)
                .IsRequired();

            cfg.Property<bool>("IsOccupied")
                .IsRequired();

            cfg.Property(p => p.No)
                .HasColumnName("AddressNo")
                .HasColumnType("nvarchar")
                .HasMaxLength(20);

            cfg.Property(s => s.Street)
                .HasColumnName("AddressStreet")
                .HasColumnType("nvarchar")
                .HasMaxLength(100);

            cfg.Property(s => s.City)
                .HasColumnName("AddressCity")
                .HasColumnType("nvarchar")
                .HasMaxLength(40);

            cfg.Property(s => s.State)
                .HasColumnName("AddressState")
                .HasColumnType("nvarchar")
                .HasMaxLength(40);

            cfg.Property(s => s.Country)
                .HasColumnName("AddressCountry")
                .HasColumnType("nvarchar")
                .HasMaxLength(40);

            cfg.Property(s => s.ZipCode)
                .HasColumnName("AddressZipCode")
                .HasColumnType("nvarchar")
                .HasMaxLength(10);

            #region TODO: Value object
            //cfg.OwnsOne(o => o.Address, b =>
            //{
            //    b.Property(p => p.No)
            //        .HasColumnName("AddressNo")
            //        .HasColumnType("nvarchar")
            //        .HasMaxLength(20)
            //        .IsRequired();

            //    b.Property(s => s.Street)
            //        .HasColumnName("AddressStreet")
            //        .HasColumnType("nvarchar")
            //        .HasMaxLength(100)
            //        .IsRequired();

            //    b.Property(s => s.City)
            //        .HasColumnName("AddressCity")
            //        .HasColumnType("nvarchar")
            //        .HasMaxLength(40)
            //        .IsRequired();

            //    b.Property(s => s.State)
            //        .HasColumnName("AddressState")
            //        .HasColumnType("nvarchar")
            //        .HasMaxLength(40)
            //        .IsRequired();

            //    b.Property(s => s.Country)
            //        .HasColumnName("AddressCountry")
            //        .HasColumnType("nvarchar")
            //        .HasMaxLength(40)
            //        .IsRequired();

            //    b.Property(s => s.ZipCode)
            //        .HasColumnName("AddressZipCode")
            //        .HasColumnType("nvarchar")
            //        .HasMaxLength(10)
            //        .IsRequired();
            //});
            #endregion

            // Ignore
            cfg.Ignore(b => b.DomainEvents);
        }
    }
}
