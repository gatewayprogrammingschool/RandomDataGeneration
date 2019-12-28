using System;
using System.Collections.Generic;
using System.Linq;
using GPS.RandomDataGenerator.Abstractions;
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace GPS.RandomDataGenerator.Generators
{
    public class GivenNameGenerator : IDataGenerator<string>
    {
        private readonly Dictionary<int, Random> _randomizers = new Dictionary<int, Random>();

        public GivenNameGenerator(IServiceProvider provider, IEnumerable<string> givenNames)
        {
            Provider = provider;
            GivenNames = givenNames.ToArray();
        }

        private IServiceProvider Provider   { get; }
        private string[]         GivenNames { get; }

        public IEnumerable<string> Generate(int? seed, int count, params object[] options)
        {
            seed ??= DateTime.Now.Millisecond;
            if (!_randomizers.TryGetValue(seed.Value, out var random))
            {
                random = new Random(seed.Value);
                _randomizers.Add(seed.Value, random);
            }

            for (var i = 0; i < count; ++i) yield return GivenNames[random.Next(0, GivenNames.Length - 1)];
        }
    }
}