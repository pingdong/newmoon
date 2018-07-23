using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PingDong.DomainDriven.Infrastructure.Mediator;
using PingDong.Newmoon.Events.Core;
using PingDong.Newmoon.Events.Infrastructure;
using PingDong.Newmoon.Events.Infrastructure.Repositories;
using PingDong.Newmoon.Events.Service.Queries;
using PingDong.QualityTools.Infrastrucutre.SqlServer;
using Xunit;

namespace PingDong.Newmoon.Events.Service.Test
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
                var eventId = queryResult[0].id;
                var result = await query.GetByIdAsync(eventId);

                // Assert
                Assert.Single(queryResult);

                Assert.Equal(DefaultName, queryResult[0].name);
                Assert.Equal(DefaultStartTime, queryResult[0].startTime);
                Assert.Equal(DefaultEndTime, queryResult[0].endTime);
                Assert.Equal(EventStatus.Created.Id, queryResult[0].statusId);

                Assert.Equal(DefaultName, result.name);
                Assert.Equal(DefaultStartTime, result.startTime);
                Assert.Equal(DefaultEndTime, result.endTime);
                Assert.Equal(EventStatus.Created.Id, result.statusId);
                Assert.Equal(DefaultPlaceId, result.placeId);

                Assert.Equal(2, result.attendees.Count);
                Assert.Equal("1", result.attendees[0].id);
                Assert.Equal("Suzy", result.attendees[0].firstname);
                Assert.Equal("Sheep", result.attendees[0].lastname);
                Assert.Equal("2", result.attendees[1].id);
                Assert.Equal("Peppa", result.attendees[1].firstname);
                Assert.Equal("Pig", result.attendees[1].lastname);
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
