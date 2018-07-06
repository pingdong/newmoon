using FluentValidation;

namespace PingDong.Newmoon.Events.Core.Validation
{
    public class AttendeeValidator : AbstractValidator<Attendee>
    {
        public AttendeeValidator()
        {
            RuleFor(a => a.Identity).NotEmpty();
        }
    }
}
