using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using PingDong.Linq;
using PingDong.Newmoon.Events.Service.Queries.Models;

namespace PingDong.Newmoon.Events.Service.Queries.Rest
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

				var sql = @"SELECT e.EventId As Id, EventName as [Name], StartTime, EndTime, PlaceId, e.StatusId
							  FROM events.Events e
							 WHERE e.EventId = @id;
							SELECT a.AttendeeId As Id, a.AttendeeFirstName as [FirstName], a.AttendeeLastName As [LastName] 
							  FROM events.Events e
					     LEFT JOIN events.Attendees a ON e.EventId = a.EventId
							 WHERE e.EventId = @id";

				var results = await connection.QueryMultipleAsync(sql, new { id });

				var events = results.Read<Event>();
				var attendees = results.Read<Attendee>().ToList();

				var evt = events.FirstOrDefault();
				if (evt != null && !attendees.IsNullOrEmpty())
					evt.Attendees = attendees;

				return evt;
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
	}
}
