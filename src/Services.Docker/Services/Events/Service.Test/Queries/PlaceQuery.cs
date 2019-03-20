﻿using System;
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

    public class PlaceQueryTest : IDisposable
    {
        private readonly string _dbName = Guid.NewGuid().ToString();

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

                await repository.Add(place);
                await repository.UnitOfWork.SaveChangesAsync();

                var query = new PlaceQuery(connectionString);

                // Act
                var queryResult = (await query.GetAllAsync()).ToList();

                // Assert
                Assert.Single(queryResult);

                Assert.Equal(DefaultName, queryResult[0].Name);
                Assert.Equal(DefaultNo, queryResult[0].AddressNo);
                Assert.Equal(DefaultStreet, queryResult[0].AddressStreet);
                Assert.Equal(DefaultCity, queryResult[0].AddressCity);
                Assert.Equal(DefaultState, queryResult[0].AddressState);
                Assert.Equal(DefaultCountry, queryResult[0].AddressCountry);
                Assert.Equal(DefaultZipCode, queryResult[0].AddressZipCode);
            });
        }

        private async void ExecuteTestCase(Func<IPlaceRepository, string, Task> action)
        {
            var options = new DbContextOptionsBuilder<EventContext>()
                                .UseSqlServer(InMemoryDbTestHelper.BuildConnectionString(_dbName))
                                .Options;

            using (var context = new EventContext(options, new EmptyMediator()))
            {
                // It's VERY important.
                await context.Database.EnsureCreatedAsync();

                var repository = new PlaceRepository(context, null);
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