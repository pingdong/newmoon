using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using PingDong.Core;

namespace PingDong.Newmoon.Events.Service.Queries
{
	public class EventQuery : IEventQuery
	{
		private readonly string _connectionString;

		public EventQuery(string connectionString)
		{
			_connectionString = string.IsNullOrWhiteSpace(connectionString) 
										? throw new ArgumentNullException(nameof(connectionString))
										: connectionString;
		}

		public async Task<Event> GetByIdAsync(int id)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();

				var result = await connection.QueryAsync<dynamic>(
					@"SELECT e.EventId As Id, EventName as [Name], StartTime, EndTime, PlaceId, e.StatusId,
							a.AttendeeId, a.AttendeeFirstName, a.AttendeeLastName
					  FROM  events.Events e
							LEFT JOIN events.Attendees a
								ON e.EventId = a.EventId
					WHERE	e.EventId = @id"
						, new { id }
					);

				return result.AsList().Count == 0 ? null : MapEvent(result);
			}
		}

		public async Task<IEnumerable<EventSummary>> GetAllAsync()
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();

				var result = await connection.QueryAsync<EventSummary>(
					@"SELECT EventId As Id, EventName as [Name], StartTime, EndTime, StatusId
						FROM events.Events");

				return result;
			}
		}

		private Event MapEvent(dynamic result)
		{
			var evt = new Event
			{
				id = result[0].Id,
				statusId = result[0].StatusId,
				name = result[0].Name,
				startTime = result[0].StartTime,
				endTime = result[0].EndTime,
				placeId = result[0].PlaceId,
				attendees = new List<Attendee>()
			};

			foreach (dynamic attendee in result)
			{
				if (!DynamicHelper.HasField(attendee, "attendeeId"))
					continue;

				var att = new Attendee
				{
					id = attendee.AttendeeId,
					firstname = attendee.AttendeeFirstName,
					lastname = attendee.AttendeeLastName,
				};

				evt.attendees.Add(att);
			}
			
			return evt;
		}
	}
}
