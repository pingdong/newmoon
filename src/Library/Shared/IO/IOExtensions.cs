using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;

using PingDong.Linq;

namespace PingDong.IO
{
    /// <summary>
    /// Extension for IO
    /// </summary>
    public static class IoExtensions
    {
        /// <summary>
        /// Find all files in the specified folder 
        /// </summary>
        /// <param name="path">Search path</param>
        /// <param name="searchPattern">Search Pattern</param>
        /// <param name="searchOption">Search Option</param>
        /// <param name="included">All types included in result only</param>
        /// <param name="excluded">All types excluded from result</param>
        /// <returns>All type that implement the specified interface</returns>
        public static IEnumerable<string> Filter(this string path,
            string searchPattern = "*",
            SearchOption searchOption = SearchOption.TopDirectoryOnly,
            IEnumerable<string> included = null,
            IEnumerable<string> excluded = null)
        {
            if (!Directory.Exists(path))
                throw new ArgumentException();

            if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
                throw new NullReferenceException(nameof(path));

            var files = Directory.EnumerateFiles(path, searchPattern, searchOption);

            if (!included.IsNullOrEmpty())
                files = files.Intersect(included, StringComparer.InvariantCultureIgnoreCase);

            if (!excluded.IsNullOrEmpty())
                files = files.Except(excluded, StringComparer.InvariantCultureIgnoreCase);

            return files;
        }

        /// <summary>
        /// Loading assemblies, that meets criteria, from the specified path
        /// </summary>
        /// <param name="path">Path to search assemblies</param>
        /// <param name="searchPattern">Search Pattern</param>
        /// <param name="searchOption">Search Option</param>
        /// <param name="included">Assemblies that will include into result</param>
        /// <param name="excluded">Assemblies that will exclude from result. Excluded overrides included</param>
        /// <exception cref="ArgumentException">Invalid Path</exception>
        /// <returns>All assemblies that meet the criteria</returns>
        public static async Task<List<Assembly>> LoadAssembliesAsync(this string path,
            string searchPattern = null,
            SearchOption searchOption = SearchOption.TopDirectoryOnly,
            IEnumerable<string> included = null,
            IEnumerable<string> excluded = null)
        {
            var files = path.Filter(searchPattern, searchOption, included, excluded);

            return await Task.Run(() =>
            {
                var found = new ConcurrentBag<Assembly>();

                foreach (var file in files)
                    found.Add(Assembly.LoadFrom(file));

                return found.ToList();
            });
        }
    }
}
