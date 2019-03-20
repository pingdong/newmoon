using System.Threading;
using System.Threading.Tasks;
using PingDong.DomainDriven.Core;

namespace PingDong.Newmoon.Events.Infrastructure.Repositories
{
    internal class UnitOfWorkMock : IUnitOfWork
    {
        public void Dispose()
        {
            
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult(0);
        }

        public Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult(true);
        }
    }
}
