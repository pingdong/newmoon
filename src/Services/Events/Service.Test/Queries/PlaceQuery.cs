using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PingDong.DomainDriven.Infrastructure.Mediator;
using PingDong.Newmoon.Events.Core;
using PingDong.Newmoon.Events.Infrastructure;
using PingDong.Newmoon.Events.Infrastructure.Repositories;
using PingDong.Newmoon.Events.Service.Queries;
using PingDong.QualityTools.Core;
using Xunit;

namespace PingDong.Newmoon.Events.Service.Test
{
    // The purpose of this unit test is a demo on how to using InMemory SQL Provider
    //   for unit testing. Without InMemory DB, 

    public class PlaceQueryTest : IDisposable
    {
        private const string DefaultName = "Place";
        private const string DefaultNo = "1";
        private const string DefaultStreet = "st.";
        private const string DefaultCity = "akl";
        private const string DefaultState = "akl";
        private const string DefaultCountry = "nz";
        private const string DefaultZipCode = "0910";

        [Fact]
        public void Add_Then_Query()
        {
            ExecuteTestCase(async (repository, connectionString) =>
            {
                // Arrange
                var place = new Core.Place(DefaultName, DefaultNo, DefaultStreet,
                    DefaultCity, DefaultState, DefaultCountry,
                    DefaultZipCode);

                repository.Add(place);
                await repository.UnitOfWork.SaveChangesAsync();

                var query = new PlaceQuery(connectionString);

                // Act
                var queryResult = (await query.GetAllAsync()).ToList();

                // Assert
                Assert.Single(queryResult);

                Assert.Equal(DefaultName, queryResult[0].name);
                Assert.Equal(DefaultNo, queryResult[0].addressNo);
                Assert.Equal(DefaultStreet, queryResult[0].addressStreet);
                Assert.Equal(DefaultCity, queryResult[0].addressCity);
                Assert.Equal(DefaultState, queryResult[0].addressState);
                Assert.Equal(DefaultCountry, queryResult[0].addressCountry);
                Assert.Equal(DefaultZipCode, queryResult[0].addressZipCode);
            });
        }

        private async void ExecuteTestCase(Func<IPlaceRepository, string, Task> action)
        {
            var options = new DbContextOptionsBuilder<EventContext>()
                                .UseSqlServer(InMemoryTestHelper.DefaultConnectionString)
                                .Options;

            using (var context = new EventContext(options, new EmptyMediator()))
            {
                // It's VERY important.
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();

                var repository = new PlaceRepository(context, null);
                var connectionString = context.Database.GetDbConnection().ConnectionString;

                await action(repository, connectionString);
            }
        }

        public void Dispose()
        {
            // Clean up the test environment
        }
    }
}
