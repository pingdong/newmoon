﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PingDong.DomainDriven.Core;
using PingDong.DomainDriven.Core.Validation;
using PingDong.Newmoon.Events.Core;

namespace PingDong.Newmoon.Events.Infrastructure.Repositories
{
    public class PlaceRepository : IPlaceRepository
    {
        private readonly EventContext _context;
        private readonly IEnumerable<IValidator<Place>> _validators;

        public IUnitOfWork UnitOfWork => _context;

        public PlaceRepository(EventContext context, IEnumerable<IValidator<Place>> validators)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _validators = validators;
        }

        public async Task<Place> FindByNameAsync(string name)
        {
            var place = await _context.Places.FirstOrDefaultAsync(p => p.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            //if (place != null)
            //{
            //    await _context.Entry(place)
            //        .Reference(p => p.Address).LoadAsync();
            //}

            return place;
        }

        public async Task<Place> GetByIdAsync(int id)
        {
            var place = await _context.Places.FindAsync(id);
            //if (place != null)
            //{
            //    await _context.Entry(place)
            //                  .Reference(p => p.Address).LoadAsync();
            //}

            return place;
        }

        public Place Add(Place place)
        {
            this._validators.Validate(place);

            if (place.IsTransient())
            {
                return _context.Places
                               .Add(place)
                               .Entity;
            }
            else
            {
                return place;
            }
        }
    }
}
