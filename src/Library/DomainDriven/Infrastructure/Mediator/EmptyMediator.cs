using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace PingDong.DomainDriven.Infrastructure.Mediator
{
    /// <summary>
    /// An empty IMediator implementation
    /// The purpose of this Mediator is used to db migration of EF and some test scenarios.
    /// It should be used only in those scenarios.
    /// </summary>
    public class EmptyMediator : IMediator
    {
        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default(CancellationToken)) where TNotification : INotification
        {
            return Task.CompletedTask;
        }

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult<TResponse>(default(TResponse));
        }

        public Task Send(IRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.CompletedTask;
        }
    }
}
