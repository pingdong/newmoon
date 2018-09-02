using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

using PingDong.Newmoon.Events.Core;
using PingDong.Newmoon.Events.Service.Commands;
using PingDong.Newmoon.Events.Service.Commands.Models;
using PingDong.Newmoon.Events.Shared;
using PingDong.QualityTools.AspNetCore;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace PingDong.Newmoon.Events
{
    public class GraphQLEventScenario : ScenarioBase
    {
        [Fact]
        public async Task Get_create_event_confirm_then_start()
        {
            var baseDir = Directory.GetCurrentDirectory() + "\\..\\..\\";

            using (var server = CreateServer(baseDir))
            {
                var http = server.CreateDefaultClient();

                // Create an event
                var createCmd = BuildCreateCommand().CreateJsonContent();
                await http.Reset().PostAsync(Api.RESTful.Post.AddEvent, createCmd);

                JObject evt = await GetCreatedEventAsync(http);
                int eventId = Convert.ToInt32(evt["data"]["events"][0]["id"]);
                string eventName = evt["data"]["events"][0]["name"].ToString();

                // Event approve and confirm
                var approveCmd = BuildApproveCommand(eventId, eventName).CreateJsonContent();
                await http.Reset().PostAsync(Api.RESTful.Post.ApproveEvent, approveCmd);
                var statusId = await GetEventStatusAsync(http, eventId);
                Assert.Equal(Convert.ToInt32(statusId), EventStatus.Approved.Id);

                var confirmCmd = BuildConfirmCommand(eventId, eventName).CreateJsonContent();
                await http.Reset().PostAsync(Api.RESTful.Post.ConfirmEvent, confirmCmd);
                statusId = await GetEventStatusAsync(http, eventId);
                Assert.Equal(Convert.ToInt32(statusId), EventStatus.Confirmed.Id);

                // Event start
                var startCmd = BuildStartCommand(eventId, eventName).CreateJsonContent();
                await http.Reset().PostAsync(Api.RESTful.Post.StartEvent, startCmd);
                statusId = await GetEventStatusAsync(http, eventId);
                Assert.Equal(Convert.ToInt32(statusId), EventStatus.Ongoing.Id);

                JObject place = await GetPlaceAsync(http);
                var isOccupied = Convert.ToBoolean(place["data"]["places"][0]["isOccupied"]);
                Assert.True(isOccupied);
            }
        }

        #region Query

        private async Task<JObject> GetCreatedEventAsync(HttpClient http)
        {
            var content = "{ \"query\": \"{ events { id name } }\" }".CreateJsonContent();
            var message = await http.PostAsync(Api.GraphQL.Post,  content);
            var response = await message.Content.ReadAsStringAsync();
            //{
            //    "events": [
            //    {
            //        "id": 51,
            //        "name": "Home"
            //    },
            //    {
            //        "id": 52,
            //        "name": "Birth Party"
            //    }
            //    ]
            //}
            return JObject.Parse(response);
        }

        private async Task<int> GetEventStatusAsync(HttpClient http, int eventId)
        {
            var content = ("{ \"query\": \"{ event(id: " + eventId + ") { id name statusId } }\" }").CreateJsonContent();
            var message = await http.PostAsync(Api.GraphQL.Post, content);
            var response = await message.Content.ReadAsStringAsync();
            //{
            //    "event": {
            //        "id": 51,
            //        "name": "Home",
            //        "statusId" 1,
            //    }
            //}
            return (int)JObject.Parse(response)["data"]["event"]["statusId"];
        }

        private async Task<JObject> GetPlaceAsync(HttpClient http)
        {
            var content = "{ \"query\": \"{ places { id name isOccupied } }\" }".CreateJsonContent();
            var message = await http.PostAsync(Api.GraphQL.Post, content);
            var response = await message.Content.ReadAsStringAsync();
            //{
            //    "places": [
            //    {
            //        "id": 51,
            //        "name": "default",
            //        "isOccupied": false
            //    }
            //    ]
            //}
            return JObject.Parse(response);
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

        private string BuildApproveCommand(int eventId, string eventName)
        {
            var cmd = new ApproveEventCommand(eventId, eventName);

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
