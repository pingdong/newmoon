﻿using System.Threading.Tasks;
using PingDong.DomainDriven.Core;

namespace PingDong.Newmoon.Events.Core
{
    public interface IEventRepository : IRepository<Event>
    {
        Task<Event> GetByIdAsync(int id);
        Event Add(Event evt);
    }
}
