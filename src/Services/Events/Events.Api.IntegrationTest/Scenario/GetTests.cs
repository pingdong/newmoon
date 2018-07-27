using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace PingDong.Newmoon.Events.Integration.Test
{
    public class GetTests : ScenarioBase
    {
        [Theory]
        [InlineData("liveness")]
        [InlineData("api/v1/events")]
        [InlineData("api/v1/places")]
        public async Task Get_get_all_events_and_response_ok_status_code(string url)
        {
            var baseDir = Directory.GetCurrentDirectory() + "\\..\\..\\";

            using (var server = CreateServer(baseDir))
            {
                var response = await server.CreateClient().GetAsync(url);

                response.EnsureSuccessStatusCode();
            }
        }
    }
}
