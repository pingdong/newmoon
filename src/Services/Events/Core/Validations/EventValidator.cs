using FluentValidation;

namespace PingDong.Newmoon.Events.Core.Validation
{
    public class EventValidator : AbstractValidator<Event>
    {
        public EventValidator()
        {
            CascadeMode = CascadeMode.Continue;

            RuleFor(evt => evt.Name).NotEmpty();

            RuleSet("Time", () =>
            {
                RuleFor(evt => evt.StartTime).NotEmpty().WithMessage("Start Time is Required");
                RuleFor(evt => evt.EndTime).NotEmpty().WithMessage("End Time is required")
                    .GreaterThan(m => m.StartTime).WithMessage("End Time must after Start Time");
            });

            RuleForEach(evt => evt.Attendees).SetValidator(new AttendeeValidator());
            //RuleFor(evt => evt.Attendees).SetCollectionValidator(new AttendeeValidator());
        }
    }
}
