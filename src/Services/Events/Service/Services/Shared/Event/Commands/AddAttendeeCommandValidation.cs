using FluentValidation;

namespace PingDong.Newmoon.Events.Service.Commands.Validation
{
    class AddAttendeeCommandValidation : AbstractValidator<AddAttendeeCommand>
    {
        public AddAttendeeCommandValidation()
        {
            RuleFor(cmd => cmd.Identity).NotEmpty();
            RuleFor(cmd => cmd.EventId).NotEmpty();
            RuleFor(cmd => cmd.FirstName).NotEmpty();
            RuleFor(cmd => cmd.LastName).NotEmpty();
        }
    }
}
