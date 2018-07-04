using System;

namespace PingDong.Data
{
    public class ObjectExistedException : Exception
    {
        public ObjectExistedException(string typeName, object duplicatedValue, string message = null)
            : base(message)
        {
            TypeName = typeName;
            DuplicatedValue = duplicatedValue;
        }

        public string TypeName { get; }
        public object DuplicatedValue { get; }
    }
}
