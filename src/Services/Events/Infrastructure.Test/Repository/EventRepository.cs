using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PingDong.DomainDriven.Infrastructure.Mediator;
using PingDong.Newmoon.Events.Core;
using PingDong.QualityTools.Core;
using Xunit;

namespace PingDong.Newmoon.Events.Infrastructure.Repositories
{
    // The purpose of this unit test is a demo on how to using InMemory SQL Provider
    //   for unit testing. Without InMemory DB, 

    public class EventRepositoryTest
    {
        private const string EventName = "Opening";
        private readonly DateTime _startTime = DateTime.Now;
        private readonly DateTime _endTime = DateTime.Now.AddHours(2);

        private const string AttendeeId = "020100";
        private const string AttendeeFirstname = "Peppa";
        private const string AttendeeLastname = "Pig";

        private const int PlaceId = 25;
        
        [Fact]
        public void Add_Then_Get()
        {
            ExecuteTestCase(async (repository, context) =>
            {
                // Arrange
                var evt = new Event(EventName, _startTime, _endTime);
                var attendee = new Attendee(AttendeeId, AttendeeFirstname, AttendeeLastname);
                evt.AddAttendee(attendee);
                evt.ChangePlace(PlaceId);
                evt.Approve();
                evt.Confirm();
                
                // Ack
                var addResult = repository.Add(evt);
                await repository.UnitOfWork.SaveChangesAsync();

                var evtRead = await context.Events.FirstAsync();
                var getResult = await repository.GetByIdAsync(evtRead.Id);

                // Assert
                Assert.Equal(getResult.Name, EventName);
                Assert.True(getResult.StartTime.Equal(_startTime));
                Assert.True(getResult.EndTime.Equal(_endTime));
                Assert.Equal(getResult.Status, EventStatus.Confirmed);
                Assert.Equal(getResult.PlaceId, PlaceId);
                Assert.Single(getResult.Attendees);
                Assert.Equal(getResult.Attendees.First().Identity, AttendeeId);
                Assert.Equal(getResult.Attendees.First().FirstName, AttendeeFirstname);
                Assert.Equal(getResult.Attendees.First().LastName, AttendeeLastname);
            });
        }

        private async void ExecuteTestCase(Func<IEventRepository, EventContext, Task> action)
        {
            var options = new DbContextOptionsBuilder<EventContext>()
                                // Randon db name for parallel testing
                                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                                //.UseSqlServer("Server=.;Database=Newmoon;User Id=Newmoon;Password=newmoon;MultipleActiveResultSets=true")
                                .Options;

            using (var context = new EventContext(options, new EmptyMediator()))
            {
                // It's VERY important.
                await context.Database.EnsureCreatedAsync();

                var repository = new EventRepository(context, null);

                await action(repository, context);
            }
        }
    }
}
