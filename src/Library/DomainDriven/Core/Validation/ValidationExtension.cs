using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using PingDong.Linq;

namespace PingDong.DomainDriven.Core.Validation
{
    public static class ValidationExtension
    {
        public static void Validate<T>(this IEnumerable<IValidator<T>> rules, T entity) where T : IAggregateRoot
        {
            if (rules == null)
                return;

            var validators = rules.ToList();
            if (validators.IsNullOrEmpty())
                return;

            var errors = new List<ValidationFailure>();

            foreach (var validator in validators)
            {
                var result = validator.Validate(entity);

                if (!result.IsValid)
                {
                    errors.AddRange(result.Errors);
                }
            }

            if (errors.Any())
                throw new ValidationException("Invalid data", errors);
        }
    }
}
