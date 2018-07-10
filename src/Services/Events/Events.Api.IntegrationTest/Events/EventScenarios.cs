using System.Threading.Tasks;
using Xunit;

namespace PingDong.Newmoon.Events.Integration.Test
{
    public class EventScenarios : EventScenarioBase
    {
        [Fact]
        public async Task Get_get_all_events_and_response_ok_status_code()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                                        .GetAsync(Get.All);

                response.EnsureSuccessStatusCode();
            }
        }
    }        
}
