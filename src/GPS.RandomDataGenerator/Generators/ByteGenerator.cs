using System;
using System.Collections.Generic;
using GPS.RandomDataGenerator.Abstractions;

namespace GPS.RandomDataGenerator.Generators
{
    public class ByteGenerator : IDataGenerator<byte>, IResetable
    {
        private Dictionary<int, Random> Randomizers { get; } = new Dictionary<int, Random>();

        public IEnumerable<byte> Generate(Random random, int count, byte min, byte max)
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

        public IEnumerable<byte> Generate(int? seed, int count, params object[] options)
        {
            if (options.Length > 0 && options[0] is byte minValue)
            {
            }
            else
            {
                minValue = byte.MinValue;
            }

            if (options.Length > 1 && options[1] is byte maxValue)
            {
            }
            else
            {
                maxValue = byte.MaxValue;
            }

            seed ??= DateTime.Now.Millisecond;
            if (!Randomizers.TryGetValue(seed.Value, out var random))
            {
                random = new Random(seed.Value);
                Randomizers.Add(seed.Value, random);
            }

            for (var i = 0; i < count; ++i) yield return (byte)random.Next(minValue, maxValue);
        }
 
        public void Reset(int seed)
        {
            if(Randomizers.ContainsKey(seed)) Randomizers.Remove(seed);
        }
    }
}