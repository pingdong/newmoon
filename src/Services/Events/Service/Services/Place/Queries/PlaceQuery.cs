using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace PingDong.Newmoon.Events.Service.Queries
{
	public class PlaceQuery : IPlaceQuery
	{
		private readonly string _connectionString;

		public PlaceQuery(string connectionString)
		{
			_connectionString = string.IsNullOrWhiteSpace(connectionString) 
										? throw new ArgumentNullException(nameof(connectionString))
										: connectionString;
		}

		public async Task<IEnumerable<Place>> GetAllAsync()
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();

				return await connection.QueryAsync<Place>("SELECT PlaceId As [Id], PlaceName As [Name], IsOccupied, AddressNo, AddressStreet, AddressCity, AddressState, AddressCountry, AddressZipCode FROM events.places");
			}
		}
	}
}
