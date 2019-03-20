using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PingDong.DomainDriven.Infrastructure.Mediator;

namespace PingDong.Newmoon.Events.Infrastructure
{
    /// <summary>
    /// This class is used in database migration of EF Core
    /// </summary>

    public class EventContextDesignFactory : IDesignTimeDbContextFactory<EventContext>
    {
        public EventContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EventContext>()
                .UseSqlServer("Server=.;Database=Newmoon;User Id=Newmoon;Password=newmoon;MultipleActiveResultSets=true");

            return new EventContext(optionsBuilder.Options, new EmptyMediator());
        }
    }
}
