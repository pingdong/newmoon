using FluentValidation;

namespace PingDong.Newmoon.Events.Core.Validations
{
    public class AttendeeValidator : AbstractValidator<Attendee>
    {
        public AttendeeValidator()
        {
            RuleFor(a => a.Identity).NotEmpty();
        }
    }
}
