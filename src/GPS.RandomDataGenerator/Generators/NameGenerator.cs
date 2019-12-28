using System;
using System.Collections.Generic;
using System.Linq;
using GPS.RandomDataGenerator.Abstractions;
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace GPS.RandomDataGenerator.Generators
{
    public class NameGenerator : IDataGenerator<string>
    {
        private Dictionary<int, Random> Randomizers { get; } = new Dictionary<int, Random>();

        public NameGenerator(IServiceProvider provider
            , IEnumerable<string> surnames
            , IEnumerable<string> givenNames)
        {
            Provider = provider;
            SurNames = surnames.ToArray();
            GivenNames = givenNames.ToArray();
        }

        private IServiceProvider Provider   { get; }
        private string[]         SurNames   { get; }
        private string[]         GivenNames { get; }

        public IEnumerable<string> Generate(int? seed, int count, params object[] options)
        {
            seed ??= DateTime.Now.Millisecond;
            if (!Randomizers.TryGetValue(seed.Value, out var random))
            {
                random = new Random(seed.Value);
                Randomizers.Add(seed.Value, random);
            }

            for (var i = 0; i < count; ++i)
                yield return
                    $"{GivenNames[random.Next(0, GivenNames.Length - 1)]} {SurNames[random.Next(0, SurNames.Length - 1)]}";
        }
    }
}