using FluentValidation.Validators;

namespace PingDong.DomainDriven.Core.Validation
{
    public class EmailFromDomainValidator : PropertyValidator
    {
        private readonly string _domain;

        public EmailFromDomainValidator(string domain)
               : base(errorMessage: "Email address {PropertyValue} is not from domain {domain}")
        {
            _domain = domain;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var split = (context.PropertyValue as string)?.Split('@');

            return split != null && split.Length == 2 && split[1].ToLower().Equals(_domain);
        }
    }
}
