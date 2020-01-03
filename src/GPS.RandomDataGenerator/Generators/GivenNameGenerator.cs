using System;
using System.Collections.Generic;
using System.Linq;
using GPS.RandomDataGenerator.Abstractions;
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace GPS.RandomDataGenerator.Generators
{
    public class GivenNameGenerator : IDataGenerator<string>, IResetable
    {
        private Dictionary<int, Random> Randomizers { get; } = new Dictionary<int, Random>();

        public GivenNameGenerator(IServiceProvider provider, IEnumerable<string> givenNames)
        {
            Provider = provider;
            GivenNames = givenNames.ToArray();
        }

        private IServiceProvider Provider   { get; }
        private string[]         GivenNames { get; }

        public IEnumerable<string> Generate(Random random, int count)
        {
            var seed = random.GetHashCode();

            if(!Randomizers.ContainsKey(seed)) Randomizers.Add(seed, random);

            return Generate(seed, count);
        }

        public IEnumerable<string> Generate(int? seed, int count, params object[] options)
        {
            seed ??= DateTime.Now.Millisecond;
            if (!Randomizers.TryGetValue(seed.Value, out var random))
            {
                random = new Random(seed.Value);
                Randomizers.Add(seed.Value, random);
            }

            for (var i = 0; i < count; ++i) yield return GivenNames[random.Next(0, GivenNames.Length - 1)];
        }
 
        public void Reset(int seed)
        {
            if(Randomizers.ContainsKey(seed)) Randomizers.Remove(seed);
        }
    }
}