using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace PingDong.Newmoon.Events.Integration.Test
{
    public class LivenessScenario : ScenarioBase
    {
        [Fact]
        public async Task Get_liveness_and_response_ok_status_code()
        {
            var baseDir = Directory.GetCurrentDirectory() + "\\..\\..\\..\\Events";

            using (var server = CreateServer(baseDir))
            {
                var response = await server.CreateClient()
                                        .GetAsync(Events.Get.Liveness);

                response.EnsureSuccessStatusCode();
            }
        }
    }        
}
