using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace PingDong.Validation
{
    /// <summary>
    /// Validation Exception
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        /// Initialise
        /// </summary>
        /// <param name="message">Error Message</param>
        /// <param name="details">Verfication error detail</param>
        /// <param name="innerException">Inner Exception</param>
        public ValidationException(string message, 
            IEnumerable<ValidationFailure> details = null, 
            Exception innerException = null)
            : base(message, innerException)
        {
            Details = details ?? new List<ValidationFailure>();
        }

        public IEnumerable<ValidationFailure> Details { get; }
    }
}
