using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GPS.RandomDataGenerator.Abstractions;

namespace GPS.RandomDataGenerator.Data
{
    public static class DataLoader
    {
        private static readonly ConcurrentDictionary<string, Assembly> Assemblies =
            new ConcurrentDictionary<string, Assembly>();

        public static IEnumerable<TData> LoadData<TData>(
            string typeName,
            string assemblyName)
        {
            if (typeName == null) throw new ArgumentNullException(nameof(typeName));

            Assemblies.TryGetValue(assemblyName, out var assembly);

            if (assembly is null)
            {
                assembly = Assembly.Load(new AssemblyName(assemblyName));

                if (assembly != null) Assemblies.TryAdd(assemblyName, assembly);
            }

            if (assembly is null) return null;

            var type = assembly.ExportedTypes.FirstOrDefault(t => t.FullName == typeName);

            if (type == null)
                throw new ArgumentException($"Could not find {typeName} in {assemblyName}.", nameof(typeName));

            var provider = Activator.CreateInstance(type) as IDataProvider<TData>;

            return provider?.Data;
        }
    }
}