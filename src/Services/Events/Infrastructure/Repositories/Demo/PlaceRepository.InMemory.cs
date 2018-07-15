using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PingDong.DomainDriven.Core;
using PingDong.DomainDriven.Core.Validation;
using PingDong.Newmoon.Events.Core;
using FluentValidation;
using Microsoft.Extensions.Caching.Memory;
using PingDong.Linq;

namespace PingDong.Newmoon.Events.Infrastructure.Repositories
{
    //*******************************************************
    //*     This is a demo on how to use IMemoryCache       *
    //*     DONOT use cache to persist data in this way     *
    //*******************************************************

    public class PlaceInMemoryRepository : IPlaceRepository
    {
        private readonly IEnumerable<IValidator<Place>> _validators;
        private readonly IMemoryCache _cache;

        private const string CacheKey = "Place.InMemory";

        public PlaceInMemoryRepository(IMemoryCache cache, IEnumerable<IValidator<Place>> validators)
        {
            _validators = validators;
            _cache = cache;
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

        private Task<Dictionary<int, Place>> TryGetValue()
        {
            var places = (Dictionary<int, Place>)_cache.Get(CacheKey);
            
            if (places.IsNullOrEmpty())
            {
                places = new Dictionary<int, Place>();
                _cache.Set(CacheKey, places);
            }

            return Task.FromResult(places);
        }

        private async Task TrySetValue(Place place)
        {
            var places = await TryGetValue();

            if (places.ContainsKey(place.Id))
                places[place.Id] = place;
            else
                places.Add(place.Id, place);

            _cache.Set(CacheKey, places);
        }

        public IUnitOfWork UnitOfWork => new UnitOfWorkMock();
    }
}
