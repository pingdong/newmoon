﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PingDong.DomainDriven.Core;
using PingDong.DomainDriven.Core.Validation;
using PingDong.Newmoon.Events.Core;
using FluentValidation;
using Newtonsoft.Json;
using PingDong.Linq;
using StackExchange.Redis;

namespace PingDong.Newmoon.Events.Infrastructure.Repositories
{
    //************************************************************
    //*     This is a demo on how to use IDistributedCache       *
    //*     DONOT use cache to persist data in this way          *
    //************************************************************

    public class PlaceRedisRepository : IPlaceRepository
    {
        private readonly IEnumerable<IValidator<Place>> _validators;
        private readonly IDatabase _cache;

        private const string CacheKey = "Place.Redis";

        public PlaceRedisRepository(ConnectionMultiplexer redis, IEnumerable<IValidator<Place>> validators)
        {
            _validators = validators;
            _cache = redis.GetDatabase();
        }

        public async Task<Place> FindByNameAsync(string name)
        {
            var cacheValue = await TryGetValue();
            var places = cacheValue.Values.ToList();

            return places.FirstOrDefault(p => p.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }

        public async Task<Place> GetByIdAsync(int id)
        {
            var places = await TryGetValue();

            if (!places.ContainsKey(id))
                throw new ArgumentOutOfRangeException($"Can't find the specified place, Id: {id}");

            return places[id];
        }

        public async Task<Place> Add(Place place)
        {
            _validators.Validate(place);

            await TrySetValue(place);

            return place;
        }

        private async Task<Dictionary<int, Place>> TryGetValue()
        {
            var cacheValue = await _cache.StringGetAsync(CacheKey);

            var places = JsonConvert.DeserializeObject<Dictionary<int, Place>>(cacheValue);

            if (places.IsNullOrEmpty())
            {
                places = new Dictionary<int, Place>();
                var saveValue = JsonConvert.SerializeObject(places);
                await _cache.StringSetAsync(CacheKey, saveValue);
            }

            return places;
        }

        private async Task TrySetValue(Place place)
        {
            var places = await TryGetValue();

            if (places.ContainsKey(place.Id))
                places[place.Id] = place;
            else
                places.Add(place.Id, place);

            var saveValue = JsonConvert.SerializeObject(places);

            await _cache.StringSetAsync(CacheKey, saveValue);
        }
        
        public IUnitOfWork UnitOfWork => new UnitOfWorkMock();
    }
}
