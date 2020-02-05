using System;
using System.Collections.Generic;
using GPS.RandomDataGenerator.Abstractions;

namespace GPS.RandomDataGenerator.Generators
{
    public class DateOffsetGenerator : IDataGenerator<DateTimeOffset>, IResetable
    {
        private Dictionary<int, Random> Randomizers { get; } = new Dictionary<int, Random>();

        public IEnumerable<DateTimeOffset> Generate(Random random, int count, DateTimeOffset min, DateTimeOffset max)
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

        public IEnumerable<DateTimeOffset> Generate(int? seed, int count, params object[] options)
        {
            if (options.Length > 0 && options[0] is DateTimeOffset minValue)
            {
            }
            else
            {
                minValue = DateTimeOffset.MinValue;
            }

            if (options.Length > 1 && options[1] is DateTimeOffset maxValue)
            {
            }
            else
            {
                maxValue = DateTimeOffset.MaxValue;
            }

            seed ??= DateTimeOffset.Now.Millisecond;
            if (!Randomizers.TryGetValue(seed.Value, out var random))
            {
                random = new Random(seed.Value);
                Randomizers.Add(seed.Value, random);
            }

            for (var i = 0; i < count; ++i)
            {
                DateTimeOffset value;
                if ((maxValue - minValue).TotalMinutes >= int.MaxValue ||
                    (maxValue - minValue).TotalMinutes <= int.MinValue)
                {
                    const int max      = int.MaxValue;
                    var       rndValue = random.Next(0, max);
                    value = minValue.AddMinutes(rndValue);
                }
                else
                {
                    var max      = (int) (maxValue - minValue).TotalMinutes;
                    var rndValue = random.Next(0, max);
                    value = minValue.AddMinutes(rndValue);
                }

                yield return value;
            }
        }
 
        public void Reset(int seed)
        {
            if(Randomizers.ContainsKey(seed)) Randomizers.Remove(seed);
        }   }
}