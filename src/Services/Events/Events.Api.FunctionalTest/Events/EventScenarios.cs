using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PingDong.Newmoon.Events.Core;
using PingDong.Newmoon.Events.Service.Commands;
using PingDong.Newmoon.Events.Service.Models;
using PingDong.QualityTools.Web;
using Xunit;

namespace PingDong.Newmoon.Events.Functional.Test
{
    public class EventScenarios : EventScenarioBase
    {
        [Fact]
        public async Task Get_create_event_confirm_then_start()
        {
            using (var server = CreateServer())
            {
                var http = server.CreateIdempotentClient();

                // Act
                var createCmd = BuildCreateCommand().CreateJsonContent();
                await http.PostAsync(Post.AddEvent, createCmd);

                dynamic evt = await GetCreatedEvent(http);

                int eventId = Convert.ToInt32(evt.id);
                string eventName = evt.name.ToString();

                var confirmCmd = BuildConfirmCommand(eventId, eventName).CreateJsonContent();
                http.RefreshRequestId();
                await http.PostAsync(Post.ConfirmEvent, confirmCmd);

                var startCmd = BuildStartCommand(eventId, eventName).CreateJsonContent();
                http.RefreshRequestId();
                await http.PostAsync(Post.StartEvent, startCmd);

                var ev = await GetEvent(http, eventId);
                dynamic place = await GetPlace(http);
                var isOccupied = Convert.ToBoolean(place.isOccupied);

                // Assert
                Assert.Equal(Convert.ToInt32(ev.value.statusId), EventStatus.Ongoing.Id);
                Assert.True(isOccupied);
            }
        }

        #region Response

        private async Task<dynamic> GetCreatedEvent(HttpClient http)
        {
            http.RefreshRequestId();
            var response = await http.GetStringAsync(Get.Events);
            dynamic result = JObject.Parse(response);
            return result.value[0];
        }

        private async Task<dynamic> GetEvent(HttpClient http, int eventId)
        {
            http.RefreshRequestId();
            var response = await http.GetStringAsync(Get.EventById(eventId));
            return JObject.Parse(response);
        }

        private async Task<dynamic> GetPlace(HttpClient http)
        {
            http.RefreshRequestId();
            var response = await http.GetStringAsync(Get.Places);
            dynamic result = JObject.Parse(response);

            return result.value[0];
        }

        #endregion

        #region Command

        private string BuildCreateCommand()
        {
            var cmd = new CreateEventCommand
            {
                Name = "Open Event",
                StartTime = DateTime.Now.AddDays(1),
                EndTime = DateTime.Now.AddHours(25),
                Attendees = new List<AttendeeDTO>(),
                Place = new PlaceDTO
                {
                    Name = "Theater",
                    Address = new AddressDTO
                    {
                        No = "20",
                        Street = "Queen st",
                        City = "Auckland",
                        State = "Akl",
                        Country = "NZ",
                        ZipCode = "0626"
                    }
                }

            };

            return JsonConvert.SerializeObject(cmd);
        }

        private string BuildConfirmCommand(int eventId, string eventName)
        {
            var cmd = new ConfirmEventCommand(eventId, eventName);

            return JsonConvert.SerializeObject(cmd);
        }

        private string BuildStartCommand(int eventId, string eventName)
        {
            var cmd = new StartEventCommand(eventId, eventName);

            return JsonConvert.SerializeObject(cmd);
        }
        
        #endregion
    }
}
