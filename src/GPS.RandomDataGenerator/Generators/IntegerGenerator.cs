using System;
using System.Collections.Generic;
using GPS.RandomDataGenerator.Abstractions;

namespace GPS.RandomDataGenerator.Generators
{
    public class IntegerGenerator : IDataGenerator<int>, IResetable
    {
        private Dictionary<int, Random> Randomizers { get; } = new Dictionary<int, Random>();

        public IEnumerable<int> Generate(Random random, int count, int min, int max)
        {
            if (!Randomizers.ContainsKey(random.GetHashCode()))
            {
                Randomizers.Add(random.GetHashCode(), random);
            }
            else
            {
                Randomizers[random.GetHashCode()] = random;
            }

            return Generate(random.GetHashCode(), count, min, max);
        }

        public IEnumerable<int> Generate(int? seed, int count, params object[] options)
        {
            if (options.Length > 0 && options[0] is int minValue)
            {
            }
            else
            {
                minValue = int.MinValue;
            }

            if (options.Length > 1 && options[1] is int maxValue)
            {
            }
            else
            {
                maxValue = int.MaxValue;
            }

            seed ??= DateTime.Now.Millisecond;
            if (!Randomizers.TryGetValue(seed.Value, out var random))
            {
                random = new Random(seed.Value);
                Randomizers.Add(seed.Value, random);
            }

            for (var i = 0; i < count; ++i) yield return random.Next(minValue, maxValue);
        }
 
        public void Reset(int seed)
        {
            if(Randomizers.ContainsKey(seed)) Randomizers.Remove(seed);
        }
    }
}