﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PingDong.Newmoon.Events.Infrastructure;

namespace PingDong.Newmoon.Events.Infrastructure.Migrations
{
    [DbContext(typeof(EventContext))]
    [Migration("20180708042058_Events-InitialCreate")]
    partial class EventsInitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("Relational:Sequence:Events.attendeseq", "'attendeseq', 'Events', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("Relational:Sequence:Events.buyerseq", "'buyerseq', 'Events', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("Relational:Sequence:Events.PlaceSeq", "'PlaceSeq', 'Events', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PingDong.DomainDriven.Core.ClientRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime>("Time");

                    b.HasKey("Id");

                    b.ToTable("Requests","Events");
                });

            modelBuilder.Entity("PingDong.Newmoon.Events.Core.Attendee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("AttendeeId")
                        .HasAnnotation("SqlServer:HiLoSequenceName", "attendeseq")
                        .HasAnnotation("SqlServer:HiLoSequenceSchema", "Events")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<int?>("EventId");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnName("AttendeeFirstName")
                        .HasMaxLength(200);

                    b.Property<string>("Identity")
                        .IsRequired()
                        .HasColumnName("AttendeeIdentity")
                        .HasMaxLength(200);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnName("AttendeeLastName")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasName("Events_Attendee_Identity");

                    b.ToTable("Attendees","Events");
                });

            modelBuilder.Entity("PingDong.Newmoon.Events.Core.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("EventId")
                        .HasAnnotation("SqlServer:HiLoSequenceName", "buyerseq")
                        .HasAnnotation("SqlServer:HiLoSequenceSchema", "Events")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<DateTime>("EndTime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("EventName");

                    b.Property<DateTime>("StartTime");

                    b.Property<int?>("StatusId");

                    b.Property<DateTime>("_createTime")
                        .HasColumnName("CreatedTime");

                    b.Property<int?>("_placeId")
                        .HasColumnName("PlaceId")
                        .HasColumnType("int");

                    b.Property<int>("_statusId");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.ToTable("Events","Events");
                });

            modelBuilder.Entity("PingDong.Newmoon.Events.Core.EventStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("StatusId")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("StatusName")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Status","Events");
                });

            modelBuilder.Entity("PingDong.Newmoon.Events.Core.Place", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("PlaceId")
                        .HasAnnotation("SqlServer:HiLoSequenceName", "PlaceSeq")
                        .HasAnnotation("SqlServer:HiLoSequenceSchema", "Events")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<string>("City")
                        .HasColumnName("AddressCity")
                        .HasMaxLength(40);

                    b.Property<string>("Country")
                        .HasColumnName("AddressCountry")
                        .HasMaxLength(40);

                    b.Property<bool>("IsOccupied");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("PlaceName")
                        .HasMaxLength(200);

                    b.Property<string>("No")
                        .HasColumnName("AddressNo")
                        .HasMaxLength(20);

                    b.Property<string>("State")
                        .HasColumnName("AddressState")
                        .HasMaxLength(40);

                    b.Property<string>("Street")
                        .HasColumnName("AddressStreet")
                        .HasMaxLength(100);

                    b.Property<string>("ZipCode")
                        .HasColumnName("AddressZipCode")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("Places","Events");
                });

            modelBuilder.Entity("PingDong.Newmoon.Events.Core.Attendee", b =>
                {
                    b.HasOne("PingDong.Newmoon.Events.Core.Event")
                        .WithMany("Attendees")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PingDong.Newmoon.Events.Core.Event", b =>
                {
                    b.HasOne("PingDong.Newmoon.Events.Core.EventStatus", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");
                });
#pragma warning restore 612, 618
        }
    }
}
