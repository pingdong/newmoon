using FluentValidation;

namespace PingDong.DomainDriven.Core.Validation
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> EmailAddressFromDomain<T>(
           this IRuleBuilder<T, string> ruleBuilder, string domain)
        {
            return ruleBuilder.SetValidator(new EmailFromDomainValidator(domain));
        }
    }
}
