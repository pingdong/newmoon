using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PingDong.EventBus.Infrastrucutre
{
    /// <summary>
    /// This class is used in database migration of EF Core
    /// </summary>

    public class EventBusLogServiceContexttDesignFactory : IDesignTimeDbContextFactory<EventBusLogServiceDbContext>
    {
        public EventBusLogServiceDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EventBusLogServiceDbContext>()
                .UseSqlServer("Server=.;Database=Newmoon;User Id=Newmoon;Password=newmoon;MultipleActiveResultSets=true");

            return new EventBusLogServiceDbContext(optionsBuilder.Options);
        }
    }
}
