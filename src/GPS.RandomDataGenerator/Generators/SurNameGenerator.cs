using System;
using System.Collections.Generic;
using System.Linq;
using GPS.RandomDataGenerator.Abstractions;
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace GPS.RandomDataGenerator.Generators
{
    public class SurNameGenerator : IDataGenerator<string>, IResetable
    {
        private Dictionary<int, Random> Randomizers { get; }= new Dictionary<int, Random>();

        public SurNameGenerator(IServiceProvider provider, IEnumerable<string> surnames)
        {
            Provider = provider;
            SurNames = surnames.ToArray();
        }

        private IServiceProvider Provider { get; }
        private string[]         SurNames { get; }

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

            for (var i = 0; i < count; ++i) yield return SurNames[random.Next(0, SurNames.Length - 1)];
        }
 
        public void Reset(int seed)
        {
            if(Randomizers.ContainsKey(seed)) Randomizers.Remove(seed);
        }
    }
}