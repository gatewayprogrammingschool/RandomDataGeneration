using System;
using System.Collections.Generic;
using System.Linq;
using GPS.RandomDataGenerator.Abstractions;
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace GPS.RandomDataGenerator.Generators
{
    public class ColorNameGenerator : IDataGenerator<string>, IResetable
    {
        private Dictionary<int, Random> Randomizers { get; } = new Dictionary<int, Random>();

        public ColorNameGenerator(IServiceProvider provider
            , IEnumerable<string> colors)
        {
            Provider = provider;
            Colors = colors.ToArray();
        }

        private string[] Colors { get; }
        private IServiceProvider Provider { get; }

        public IEnumerable<string> Generate(Random random, int count)
        {
            var seed = random.GetHashCode();

            if (!Randomizers.ContainsKey(seed)) Randomizers.Add(seed, random);

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

            for (var i = 0; i < count; ++i) yield return Colors[random.Next(0, Colors.Length - 1)];
        }

        public void Reset(int seed)
        {
            if(Randomizers.ContainsKey(seed)) Randomizers.Remove(seed);
        }
    }
}