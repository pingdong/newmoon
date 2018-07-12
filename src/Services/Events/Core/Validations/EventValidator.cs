using System;
using FluentValidation;

namespace PingDong.Newmoon.Events.Core.Validation
{
    public class EventValidator : AbstractValidator<Event>
    {
        public EventValidator()
        {
            CascadeMode = CascadeMode.Continue;

            RuleFor(evt => evt.Name).NotEmpty();

            RuleFor(evt => evt.StartTime).NotEmpty().WithMessage("Start Time is Required")
                .Must(BeAValidStartDate).WithMessage("Not early than 9:00AM");

            RuleFor(evt => evt.EndTime).NotEmpty().WithMessage("End Time is required")
                .GreaterThan(m => m.StartTime).WithMessage("End Time must after Start Time");

            RuleForEach(evt => evt.Attendees).SetValidator(new AttendeeValidator());
        }

        private bool BeAValidStartDate(DateTime date)
        {
            return date.Hour >= 9;
        }
    }
}
