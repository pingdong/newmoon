using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

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
				   @"SELECT e.EventId As Id, EventName as [Name], StartTime, EndTime, PlaceName, s.StatusName,
							a.AttendeeId, a.AttendeeFirstName, a.AttendeeLastName
					  FROM  events.Events e
							INNER JOIN events.Places p 
								ON e.PlaceId = p.PlaceId
							INNER JOIN events.Attendees a
								ON e.EventId = a.EventId
							INNER JOIN events.[Status] s
								ON e.StatusId = s.StatusId
					WHERE	e.EventId = @id"
						, new { id }
					);

			    return result.AsList().Count == 0 ? null : MapEvent(result);
			}
		}

		private Event MapEvent(dynamic result)
		{
			var evt = new Event
			{
				id = result[0].Id,
				status = result[0].StatusName,
				name = result[0].Name,
				startTime = result[0].StartTime,
				endTime = result[0].EndTime,
				place = result[0].PlaceName,
				attendees = new List<Attendee>()
			};

			foreach (dynamic attendee in result)
			{
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

		public async Task<IEnumerable<EventSummary>> GetAllAsync()
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();

				return await connection.QueryAsync<EventSummary>(
					@"SELECT EventId As Id, EventName as [Name], StartTime, EndTime, StatusName as [Status] 
						FROM events.Events a 
								INNER JOIN events.[Status] b 
							ON a.StatusId = b.StatusId");
			}
		}
	}
}
