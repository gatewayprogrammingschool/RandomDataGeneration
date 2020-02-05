using System;
using System.Collections.Generic;
using GPS.RandomDataGenerator.Abstractions;

namespace GPS.RandomDataGenerator.Generators
{
    public class UnsignedIntegerGenerator : IDataGenerator<uint>, IResetable
    {
        private Dictionary<int, Random> Randomizers { get; } = new Dictionary<int, Random>();

        public IEnumerable<uint> Generate(Random random, int count, uint min, uint max)
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

        public IEnumerable<uint> Generate(int? seed, int count, params object[] options)
        {
            if (options.Length > 0 && options[0] is uint minValue)
            {
            }
            else
            {
                minValue = uint.MinValue;
            }

            if (options.Length > 1 && options[1] is uint maxValue)
            {
            }
            else
            {
                maxValue = uint.MaxValue;
            }

            var spread = int.MinValue + (int)(maxValue - minValue);

            var max = spread;
            var min = int.MinValue;

            seed ??= DateTime.Now.Millisecond;
            if (!Randomizers.TryGetValue(seed.Value, out var random))
            {
                random = new Random(seed.Value);
                Randomizers.Add(seed.Value, random);
            }

            for (var i = 0; i < count; ++i)
            {
                uint value = (uint)((long)random.Next(min, max) - int.MinValue);
                yield return value;
            }
        }

        public void Reset(int seed)
        {
            if (Randomizers.ContainsKey(seed)) Randomizers.Remove(seed);
        }
    }
}