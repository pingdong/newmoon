using System;

namespace PingDong.DomainDriven.Service.GraphQL
{
    /// <summary>
    /// The attribute is used for dynamic loading GraphQL types
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class GraphTypeAttribute : Attribute
    {
    }
}
