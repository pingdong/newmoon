using System.Threading.Tasks;
using PingDong.Newmoon.Events.Integration.Test.Shared;
using Xunit;

namespace PingDong.Newmoon.Events.Integration.Test
{
    public class GetTestsWithWebAppFactory : IClassFixture<EventsWebApplicationFactory>
    {
        private readonly EventsWebApplicationFactory _factory;

        public GetTestsWithWebAppFactory(EventsWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("liveness")]
        [InlineData("api/v1/events")]
        [InlineData("api/v1/places")]
        public async Task Get_and_response_ok_status_code(string url)
        {
            // Arrange
            var http = _factory.CreateClient();

            // Act
            var response = await http.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
