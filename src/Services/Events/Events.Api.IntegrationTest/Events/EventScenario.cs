using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace PingDong.Newmoon.Events.Integration.Test
{
    public class EventScenario : ScenarioBase
    {
        [Fact]
        public async Task Get_get_all_events_and_response_ok_status_code()
        {
            var baseDir = Directory.GetCurrentDirectory() + "\\..\\..\\..\\Events";

            using (var server = CreateServer(baseDir))
            {
                var response = await server.CreateClient()
                                        .GetAsync(Events.Get.All);

                response.EnsureSuccessStatusCode();
            }
        }
    }        
}
