using FluentValidation;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PingDong.Newmoon.Events.Core.Exceptions;

namespace Ordering.API.Infrastructure.Behaviors
{
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        // For demostration purpose only, request is validated in the ASP.Net pipeline before hitting controller.
        // MediatR behavior happens after call send in controller

        private readonly IValidator<TRequest>[] _validators;
        public ValidatorBehavior(IValidator<TRequest>[] validators) => _validators = validators;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var failures = _validators
                                .Select(v => v.Validate(request))
                                .SelectMany(result => result.Errors)
                                .Where(error => error != null)
                                .ToList();

            if (failures.Any())
            {
                throw new EventDomainException(
                    $"Command Validation Errors for type {typeof(TRequest).Name}", new ValidationException("Validation exception", failures));
            }

            var response = await next();
            return response;
        }
    }
}
