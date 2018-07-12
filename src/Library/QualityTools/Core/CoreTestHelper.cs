using System.Linq;
using System.Reflection;

namespace PingDong.QualityTools.Core
{
    public static class ObjectTestHelper
    {
        public static BindingFlags Flags = BindingFlags.Instance
                                           | BindingFlags.GetProperty
                                           | BindingFlags.SetProperty
                                           | BindingFlags.GetField
                                           | BindingFlags.SetField
                                           | BindingFlags.NonPublic;

        /// <summary>
        /// A static method to get the PropertyInfo of a private property of any object.
        /// </summary>
        /// <param name="target">The target object</param>
        /// <param name="propertyName">The name of the private property</param>
        /// <returns>PropertyInfo object. It has the property name and a useful GetValue() method.</returns>
        public static PropertyInfo GetPrivatePropertyInfo(this object target, string propertyName)
        {
            var props = target.GetType().GetProperties(Flags);
            return props.FirstOrDefault(propInfo => propInfo.Name == propertyName);
        }

        /// <summary>
        /// A static method to get the value of a private property of any object.
        /// </summary>
        /// <param name="target">The instance from which to read the private value.</param>
        /// <param name="propertyName">The name of the private property</param>
        /// <returns>The value of the property boxed as an object.</returns>
        public static object GetPrivatePropertyValue(this object target, string propertyName)
        {
            return target.GetPrivatePropertyInfo(propertyName).GetValue(target);
        }

        /// <summary>
        /// A static method to get the value of a private property of any object.
        /// </summary>
        /// <param name="target">The instance from which to read the private value.</param>
        /// <param name="propertyName">The name of the private property</param>
        /// <returns>The value of the property boxed as an object.</returns>
        public static T GetPrivatePropertyValue<T>(this object target, string propertyName)
        {
            return (T)target.GetPrivatePropertyInfo(propertyName).GetValue(target);
        }

        /// <summary>
        /// A static method to get the FieldInfo of a private field of any object.
        /// </summary>
        /// <param name="target">The target object</param>
        /// <param name="fieldName">The name of the private field</param>
        /// <returns>FieldInfo object. It has the field name and a useful GetValue() method.</returns>
        public static FieldInfo GetPrivateFieldInfo(this object target, string fieldName)
        {
            var fields = target.GetType().GetFields(Flags);
            return fields.FirstOrDefault(feildInfo => feildInfo.Name == fieldName);
        }

        /// <summary>
        /// A static method to get the FieldInfo of a private field of any object.
        /// </summary>
        /// <param name="fieldName">The name of the private field</param>
        /// <param name="target">The instance from which to read the private value.</param>
        /// <returns>The value of the property boxed as an object.</returns>
        public static object GetPrivateFieldValue(this object target, string fieldName)
        {
            return target.GetPrivateFieldInfo(fieldName).GetValue(target);
        }

        /// <summary>
        /// A static method to get the FieldInfo of a private field of any object.
        /// </summary>
        /// <param name="fieldName">The name of the private field</param>
        /// <param name="target">The instance from which to read the private value.</param>
        /// <returns>The value of the property boxed as an object.</returns>
        public static T GetPrivateFieldValue<T>(this object target, string fieldName)
        {
            return (T)target.GetPrivateFieldInfo(fieldName).GetValue(target);
        }
    }
}
