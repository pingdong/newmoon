using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using PingDong.Newmoon.Events.Core;
using PingDong.Newmoon.Events.Service.Commands;
using PingDong.Newmoon.Events.Service.Models;
using Xunit;

namespace PingDong.Newmoon.Events.Service.Test
{
    public class CreatePlaceCommandHandlerTest : IDisposable
    {
        private const string DefaultName = "Place";
        private const string DefaultNo = "1";
        private const string DefaultStreet = "st.";
        private const string DefaultCity = "akl";
        private const string DefaultState = "akl";
        private const string DefaultCountry = "nz";
        private const string DefaultZipCode = "0910";


        [Fact]
        public async void Handle()
        {
            // Arrange
            var repositoryMock = new Mock<IPlaceRepository>();

            Place savedPlace = null;

            repositoryMock.Setup(repository => repository.Add(It.IsAny<Place>()))
                            .Returns<Place>(x => x)
                            .Callback<Place>(r => savedPlace = r);
            repositoryMock.Setup(repository => repository.UnitOfWork.SaveEntitiesAsync(It.IsAny<CancellationToken>()))
                            .Returns(Task.FromResult(true));

            var handler = new CreatePlaceCommandHandler(repositoryMock.Object, null);
            
            // Act
            var msg = new CreatePlaceCommand(DefaultName, new AddressDTO(DefaultNo, DefaultStreet, DefaultCity, DefaultState, DefaultCountry, DefaultZipCode));
            var token = new CancellationToken();
            var result = await handler.Handle(msg, token);

            // Assert
            Assert.True(result);
            // Repository.Add is called
            repositoryMock.Verify(p => p.Add(It.IsAny<Place>()), Times.Once);
            // SaveEntitiesAsync is called
            repositoryMock.Verify(p => p.UnitOfWork.SaveEntitiesAsync(It.IsAny<CancellationToken>()), Times.Once);
            // Verify Saved value
            Assert.NotNull(savedPlace);
            Assert.Equal(DefaultName, savedPlace.Name);
            Assert.Equal(DefaultNo, savedPlace.No);
            Assert.Equal(DefaultStreet, savedPlace.Street);
            Assert.Equal(DefaultCity, savedPlace.City);
            Assert.Equal(DefaultState, savedPlace.State);
            Assert.Equal(DefaultCountry, savedPlace.Country);
            Assert.Equal(DefaultZipCode, savedPlace.ZipCode);

            repositoryMock.VerifyNoOtherCalls();
        }


        public void Dispose()
        {
            // Clean up the test environment
        }
    }
}
