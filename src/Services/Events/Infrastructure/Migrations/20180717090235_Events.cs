using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PingDong.Newmoon.Events.Infrastructure.Migrations
{
    public partial class Events : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "events");

            migrationBuilder.CreateSequence(
                name: "AttendeSeq",
                schema: "events",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "EventSeq",
                schema: "events",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "PlaceSeq",
                schema: "events",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Events",
                schema: "events",
                columns: table => new
                {
                    EventId = table.Column<int>(nullable: false),
                    EventName = table.Column<string>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    PlaceId = table.Column<int>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    StatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                });

            migrationBuilder.CreateTable(
                name: "Places",
                schema: "events",
                columns: table => new
                {
                    PlaceId = table.Column<int>(nullable: false),
                    AddressNo = table.Column<string>(maxLength: 20, nullable: true),
                    AddressStreet = table.Column<string>(maxLength: 100, nullable: true),
                    AddressCity = table.Column<string>(maxLength: 40, nullable: true),
                    AddressState = table.Column<string>(maxLength: 40, nullable: true),
                    AddressCountry = table.Column<string>(maxLength: 40, nullable: true),
                    AddressZipCode = table.Column<string>(maxLength: 10, nullable: true),
                    PlaceName = table.Column<string>(maxLength: 200, nullable: false),
                    IsOccupied = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.PlaceId);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                schema: "events",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attendees",
                schema: "events",
                columns: table => new
                {
                    AttendeeId = table.Column<int>(nullable: false),
                    AttendeeIdentity = table.Column<string>(maxLength: 200, nullable: false),
                    AttendeeFirstName = table.Column<string>(maxLength: 200, nullable: false),
                    AttendeeLastName = table.Column<string>(maxLength: 200, nullable: false),
                    EventId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendees", x => x.AttendeeId);
                    table.ForeignKey(
                        name: "FK_Attendees_Events_EventId",
                        column: x => x.EventId,
                        principalSchema: "events",
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendees_EventId",
                schema: "events",
                table: "Attendees",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "Events_Attendee_Identity",
                schema: "events",
                table: "Attendees",
                column: "AttendeeId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendees",
                schema: "events");

            migrationBuilder.DropTable(
                name: "Places",
                schema: "events");

            migrationBuilder.DropTable(
                name: "Requests",
                schema: "events");

            migrationBuilder.DropTable(
                name: "Events",
                schema: "events");

            migrationBuilder.DropSequence(
                name: "AttendeSeq",
                schema: "events");

            migrationBuilder.DropSequence(
                name: "EventSeq",
                schema: "events");

            migrationBuilder.DropSequence(
                name: "PlaceSeq",
                schema: "events");
        }
    }
}
