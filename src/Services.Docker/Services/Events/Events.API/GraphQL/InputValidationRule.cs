using GraphQL.Validation;

namespace PingDong.Newmoon.Events.GraphQL
{
    /// <summary>
    /// Validation for input data
    ///
    /// Demo purpose only
    /// </summary>
    public class InputValidationRule : IValidationRule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public INodeVisitor Validate(ValidationContext context)
        {
            return new EnterLeaveListener(option => { });
        }
    }
}
