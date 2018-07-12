using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PingDong.DomainDriven.Core;

namespace PingDong.DomainDriven.Infrastructure
{
    static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync<T>(this IMediator mediator, T ctx) where T: DbContext
        {
            var domainEntities = ctx.ChangeTracker
                                    .Entries<Entity>()
                                    .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
                                    .ToList();

            var domainEvents = domainEntities.SelectMany(x => x.Entity.DomainEvents)
                                             .ToList();

            domainEntities.ToList()
                          .ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents.Select(async domainEvent => {
                                                await mediator.Publish(domainEvent);
                                            });

            await Task.WhenAll(tasks);
        }
    }
}
