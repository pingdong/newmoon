using Microsoft.CSharp.RuntimeBinder;

namespace PingDong
{
    public static class DynamicHelper
    {
        public static bool HasField(dynamic obj, string field)
        {
            bool retval = false;
            try
            {
                // can't write the following:
                var temp = obj[field];
                retval = true;
            }
            catch (RuntimeBinderException) { }

            return retval;
        }
    }
}
