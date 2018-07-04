using System;

namespace PingDong.Reflection
{
    /// <summary>
    /// Extension to load all assemblies that meet the criteria
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Get the directory name of the assembly that type belongs
        /// </summary>
        /// <param name="type">Specified type</param>
        /// <returns>The directory of the type</returns>
        public static string GetDirectoryName(this Type type)
        {
            return type.Assembly.GetDirectoryName();
        }
    }
}
