using System;
using System.Collections.Generic;
using GPS.RandomDataGenerator.Abstractions;

namespace GPS.RandomDataGenerator.Generators
{
    public class DecimalGenerator : IDataGenerator<decimal>, IResetable
    {
        private Dictionary<int, Random> Randomizers { get; } = new Dictionary<int, Random>();

        public IEnumerable<decimal> Generate(Random random, int count, decimal min, decimal max)
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

        public IEnumerable<decimal> Generate(int? seed, int count, params object[] options)
        {
            if (options.Length > 0 && options[0] is decimal minValue)
            {
            }
            else
            {
                minValue = decimal.MinValue;
            }

            if (options.Length > 1 && options[1] is decimal maxValue)
            {
            }
            else
            {
                maxValue = decimal.MaxValue;
            }

            seed ??= DateTime.Now.Millisecond;
            if (!Randomizers.TryGetValue(seed.Value, out var random))
            {
                random = new Random(seed.Value);
                Randomizers.Add(seed.Value, random);
            }

            var overflow = true;
            try
            {
                // ReSharper disable once AssignmentIsFullyDiscarded
                _ = maxValue - minValue;
                overflow = false;
            }
            catch
            {
                // ignored
            }

            for (var i = 0; i < count; ++i)
                if (overflow)
                {
                    var rnd  = (decimal) random.NextDouble();
                    var flip = random.NextDouble() > 0.5 ? 0 : 1;
                    yield return rnd * maxValue + minValue * flip;
                }
                else
                {
                    var rnd = (decimal) random.NextDouble();
                    yield return rnd * (maxValue - minValue) + minValue;
                }
        }
 
        public void Reset(int seed)
        {
            if(Randomizers.ContainsKey(seed)) Randomizers.Remove(seed);
        }   }
}