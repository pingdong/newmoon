using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using PingDong.Linq;

namespace PingDong.Reflection
{
    /// <summary>
    /// Extension to load all assemblies that meet the criteria
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Get directory name of the specified assembly
        /// </summary>
        /// <param name="assembly">Specified assembly</param>
        /// <returns>The directory of the specified assembly</returns>
        public static string GetDirectoryName(this Assembly assembly)
        {
            var codeBase = assembly.CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);

            return Path.GetDirectoryName(path);
        }

        #region Find Reference Assembly

        /// <summary>
        /// Load referenced assemblies of the specified assembly
        /// </summary>
        /// <param name="assembly">Target assembly</param>
        /// <param name="prefix">Prefix</param>
        /// <param name="includeIndirectReference">Whether including indirectly referenced assemblies</param>
        /// <returns></returns>
        public static List<Assembly> GetReferenceAssemblies(this Assembly assembly, string prefix = null, bool includeIndirectReference = true)
        {
            return GetReferenceAssemblies(assembly, new List<string> { prefix }, includeIndirectReference);
        }

        /// <summary>
        /// Load referenced assemblies of the specified assembly
        /// </summary>
        /// <param name="assembly">Target assembly</param>
        /// <param name="prefix">Prefix</param>
        /// <param name="includeIndirectReference">Whether including indirectly referenced assemblies</param>
        public static List<Assembly> GetReferenceAssemblies(this Assembly assembly, IList<string> prefix = null, bool includeIndirectReference = true)
        {
            var assembiles = new List<Assembly> { assembly };

            if (!prefix.IsNullOrEmpty())
            {
                var assembieNames = assembly.GetReferencedAssemblies();

                var names = prefix.IsNullOrEmpty()
                    ? assembieNames
                    : assembieNames.Where(
                        a => prefix.Any(p => a.Name.StartsWith(p, StringComparison.OrdinalIgnoreCase)));

                foreach (var file in names)
                {
                    var target = Assembly.Load(file);

                    // Loading all indirect referenced files
                    if (includeIndirectReference)
                        LoadAssemblies(target, assembiles, prefix);

                    if (!assembiles.Contains(target))
                        assembiles.Add(target);
                }
            }

            return assembiles;
        }
        
        private static void LoadAssemblies(Assembly assembly, IList<Assembly> assemblies, IList<string> prefix = null)
        {
            var assembieNames = assembly.GetReferencedAssemblies();

            var files = prefix.IsNullOrEmpty()
                ? assembieNames
                : assembieNames.Where(a => prefix.Any(p => a.Name.StartsWith(p, StringComparison.OrdinalIgnoreCase)));

            var target = files.ToList();

            if (!target.Any())
                return;

            foreach (var file in target)
            {
                var a = Assembly.Load(file);

                if (!assemblies.Contains(a))
                    assemblies.Add(a);
            }
        }

        #endregion

        #region Type Discovery

        #region Interface
        /// <summary>
        /// Find all instances of the specific interface in current folder 
        /// </summary>
        /// <typeparam name="TInterface">Specified interface</typeparam>
        /// <param name="assemblies">Target assemblies</param>
        /// <returns>All type that implement the specified interface</returns>
        public static List<TInterface> FindInterfaces<TInterface>(this IEnumerable<Assembly> assemblies)
        {
            return Find<TInterface>(assemblies, isInterface: true);
        }

        /// <summary>
        /// Find all instances of the specific interface in current folder 
        /// </summary>
        /// <typeparam name="TInterface">Specified interface</typeparam>
        /// <param name="assembly">Target assembly</param>
        /// <returns>All type that implement the specified interface</returns>
        public static List<TInterface> FindInterfaces<TInterface>(this Assembly assembly)
        {
            return Find<TInterface>(assembly, isInterface: true);
        }

        #region Private
        private static List<TTarget> Find<TTarget>(IEnumerable<Assembly> assemblies, bool isInterface)
        {
            var found = new List<TTarget>();

            foreach (var assembly in assemblies)
            {
                var types = FindTypes<TTarget>(assembly, isInterface);

                if (!types.IsNullOrEmpty())
                {
                    types.ForEach(type => found.Add((TTarget)Activator.CreateInstance(type)));
                }
            }

            return found;
        }

        private static List<TTarget> Find<TTarget>(Assembly assembly, bool isInterface)
        {
            var found = new List<TTarget>();

            var types = FindTypes<TTarget>(assembly, isInterface);

            if (!types.IsNullOrEmpty())
            {
                types.ForEach(type => found.Add((TTarget)Activator.CreateInstance(type)));
            }

            return found;
        }

        private static List<Type> FindTypes<TTarget>(Assembly assembly, bool isInterface)
        {
            var types = assembly.GetTypes()
                .Where(t => t.IsClass);
            if (isInterface)
            {
                types = types.Where(t => !t.IsAbstract)
                    .Where(t => typeof(TTarget).IsAssignableFrom(t));
            }
            else
            {
                types = types.Where(t => t.IsSubclassOf(typeof(TTarget)));
            }

            return types.ToList();
        }
        #endregion

        #endregion

        #region Base class
        /// <summary>
        /// Find all instances of the specific Base class in current folder 
        /// </summary>
        /// <typeparam name="TBase">Specified Base class</typeparam>
        /// <param name="assemblies">Target assemblies</param>
        /// <returns>All type that implement the specified Base class</returns>
        public static List<TBase> FindSubclasses<TBase>(this IEnumerable<Assembly> assemblies)
        {
            return Find<TBase>(assemblies, isInterface: false);
        }

        /// <summary>
        /// Find all instances of the specific Base class in current folder 
        /// </summary>
        /// <typeparam name="TBase">Specified Base class</typeparam>
        /// <param name="assembly">Target assembly</param>
        /// <returns>All type that implement the specified Base class</returns>
        public static List<TBase> FindSubclasses<TBase>(this Assembly assembly)
        {
            return Find<TBase>(assembly, isInterface: false);
        }
        #endregion

        #region Attribute

        /// <summary>
        /// Find types that has specified attribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assemblies"></param>
        /// <param name="attribute"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static List<Type> FindAttribute<T>(this IEnumerable<Assembly> assemblies, Type attribute, Func<T, bool> condition = null) where T : Attribute
        {
            var found = new List<Type>();

            foreach (var assembly in assemblies)
            {
                var types = assembly.FindAttribute(attribute, condition);

                found.AddRange(types);
            }

            return found;
        }

        /// <summary>
        /// Find type that has specified attribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assembly"></param>
        /// <param name="attribute"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static List<Type> FindAttribute<T>(this Assembly assembly, Type attribute, Func<T, bool> condition = null) where T : Attribute
        {
            var types = assembly.GetTypes().Where(t => t.IsClass);
            types = types.Where(t =>
            {
                var att = Attribute.GetCustomAttribute(t, typeof(T));
                if (att == null)
                    return false;

                if (condition != null)
                {
                    var target = (T)att;
                    return condition(target);
                }

                return true;
            });

            return types.ToList();
        }

        #endregion

        #endregion
    }
}

