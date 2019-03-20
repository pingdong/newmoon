using System.IO;
using System.Threading.Tasks;
using PingDong.Newmoon.Events.Shared;
using PingDong.QualityTools.AspNetCore;
using Xunit;

namespace PingDong.Newmoon.Events
{
    public class GraphQLGetTests : ScenarioBase
    {
        [Theory]
        [InlineData("api/graphql")]
        public async Task Get_get_all_events_and_response_ok_status_code(string url)
        {
            var baseDir = Directory.GetCurrentDirectory() + "\\..\\..\\";

            using (var server = CreateServer(baseDir))
            {
                var query = "{ \"query\": \"{ places { id name } }\" }";

                var response = await server.CreateClient().PostAsync(url, query.CreateJsonContent());

                response.EnsureSuccessStatusCode();
            }
        }
    }
}
