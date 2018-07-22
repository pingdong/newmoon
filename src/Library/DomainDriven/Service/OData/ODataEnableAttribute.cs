using System;

namespace PingDong.DomainDriven.Service.OData
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class ODataEnableAttribute : Attribute
    {
        public bool Enabled { get; }

        public string Name { get; }

        public ODataEnableAttribute(string name, bool enable = true)
        {
            Enabled = enable;
            Name = name;
        }
    }
}
