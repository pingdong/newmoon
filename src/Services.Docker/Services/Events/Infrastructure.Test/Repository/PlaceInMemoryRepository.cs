using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using PingDong.Newmoon.Events.Core;
using Xunit;

namespace PingDong.Newmoon.Events.Infrastructure.Repositories
{
    // The purpose of this unit test is a demo on how to using InMemory SQL Provider
    //   for unit testing, without SQLServer Express InMemory DB provider. 

    public class PlaceInMemoryRepositoryTest
    {
        private const string DefaultName = "Place";
        private const string DefaultNo = "1";
        private const string DefaultStreet = "st.";
        private const string DefaultCity = "akl";
        private const string DefaultState = "akl";
        private const string DefaultCountry = "nz";
        private const string DefaultZipCode = "0910";
        
        [Fact]
        public void Add_Then_Get_InMemory()
        {
            ExecuteTestCase(async repository =>
            {
                // Arrange
                // Suprise, a variable can be named in Simplified Chinese!!
                // Just for fun
                var 某地点 = new Place(DefaultName, DefaultNo, DefaultStreet,
                    DefaultCity, DefaultState, DefaultCountry,
                    DefaultZipCode);

                // Ack
                var addResult = await repository.Add(某地点);
                await repository.UnitOfWork.SaveChangesAsync();

                var getResult = await repository.GetByIdAsync(addResult.Id);

                // Assert
                Assert.Equal(DefaultName, addResult.Name);
                Assert.Equal(DefaultName, getResult.Name);
                Assert.Equal(DefaultNo, addResult.No);
                Assert.Equal(DefaultNo, getResult.No);
                Assert.Equal(DefaultStreet, addResult.Street);
                Assert.Equal(DefaultStreet, getResult.Street);
                Assert.Equal(DefaultCity, addResult.City);
                Assert.Equal(DefaultCity, getResult.City);
                Assert.Equal(DefaultState, addResult.State);
                Assert.Equal(DefaultState, getResult.State);
                Assert.Equal(DefaultCountry, addResult.Country);
                Assert.Equal(DefaultCountry, getResult.Country);
                Assert.Equal(DefaultZipCode, addResult.ZipCode);
                Assert.Equal(DefaultZipCode, getResult.ZipCode);
            });
        }

        private async void ExecuteTestCase(Func<IPlaceRepository, Task> action)
        {
            var option = new MemoryCacheOptions();
            var cache = new MemoryCache(option);
            var repository = new PlaceInMemoryRepository(cache, null);

            await action(repository);
        }
    }
}
