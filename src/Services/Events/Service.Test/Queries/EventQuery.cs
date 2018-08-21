using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PingDong.DomainDriven.Infrastructure.Mediator;
using PingDong.Newmoon.Events.Core;
using PingDong.Newmoon.Events.Infrastructure;
using PingDong.Newmoon.Events.Infrastructure.Repositories;
using PingDong.Newmoon.Events.Service.Queries.Rest;
using PingDong.QualityTools.Infrastrucutre.SqlServer;
using Xunit;

namespace PingDong.Newmoon.Events.Service.Queries.Rest
{
    // The purpose of this unit test is a demo on how to using InMemory SQL Provider
    //   for unit testing. Without InMemory DB, 

    public class EventQueryTest : IDisposable
    {
        private readonly string _dbName = Guid.NewGuid().ToString();

        private const string DefaultName = "Place";
        private readonly DateTime DefaultStartTime = DateTime.Now;
        private readonly DateTime DefaultEndTime = DateTime.Now.AddHours(2);
        private const int DefaultPlaceId = 16;
        
        [Fact]
        public void Add_Then_Query()
        {
            ExecuteTestCase(async (repository, connectionString) =>
            {
                // Arrange
                var evt = new Core.Event(DefaultName, DefaultStartTime, DefaultEndTime);
                evt.ChangePlace(DefaultPlaceId);
                evt.AddAttendee(new Core.Attendee("1", "Suzy", "Sheep"));
                evt.AddAttendee(new Core.Attendee("2", "Peppa", "Pig"));

                repository.Add(evt);
                await repository.UnitOfWork.SaveChangesAsync();

                var query = new EventQuery(connectionString);

                // Act
                var queryResult = (await query.GetAllAsync()).ToList();
                var eventId = queryResult[0].Id;
                var result = await query.GetByIdAsync(eventId);

                // Assert
                Assert.Single(queryResult);

                Assert.Equal(DefaultName, queryResult[0].Name);
                Assert.Equal(DefaultStartTime, queryResult[0].StartTime);
                Assert.Equal(DefaultEndTime, queryResult[0].EndTime);
                Assert.Equal(EventStatus.Created.Id, queryResult[0].StatusId);

                Assert.Equal(DefaultName, result.Name);
                Assert.Equal(DefaultStartTime, result.StartTime);
                Assert.Equal(DefaultEndTime, result.EndTime);
                Assert.Equal(EventStatus.Created.Id, result.StatusId);
                Assert.Equal(DefaultPlaceId, result.PlaceId);

                Assert.Equal(2, result.Attendees.Count);
                Assert.Equal("1", result.Attendees[0].Id);
                Assert.Equal("Suzy", result.Attendees[0].Firstname);
                Assert.Equal("Sheep", result.Attendees[0].Lastname);
                Assert.Equal("2", result.Attendees[1].Id);
                Assert.Equal("Peppa", result.Attendees[1].Firstname);
                Assert.Equal("Pig", result.Attendees[1].Lastname);
            });
        }

        private async void ExecuteTestCase(Func<IEventRepository, string, Task> action)
        {
            var options = new DbContextOptionsBuilder<EventContext>()
                                .UseSqlServer(InMemoryDbTestHelper.BuildConnectionString(_dbName))
                                .Options;

            using (var context = new EventContext(options, new EmptyMediator()))
            {
                // It's VERY important.
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();

                var repository = new EventRepository(context, null);
                var connectionString = context.Database.GetDbConnection().ConnectionString;

                await action(repository, connectionString);
            }
        }

        public void Dispose()
        {
            // Clean up the test environment

            // Removing physic db file
            InMemoryDbTestHelper.CleanUp(_dbName);
        }
    }
}
