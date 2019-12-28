using System;
using System.Collections.Generic;
using GPS.RandomDataGenerator.Abstractions;

namespace GPS.RandomDataGenerator.Generators
{
    public class DateGenerator : IDataGenerator<DateTime>
    {
        private readonly Dictionary<int, Random> _randomizers = new Dictionary<int, Random>();

        public IEnumerable<DateTime> Generate(int? seed, int count, params object[] options)
        {
            if (options.Length > 0 && options[0] is DateTime minValue)
            {
            }
            else
            {
                minValue = DateTime.MinValue;
            }

            if (options.Length > 1 && options[1] is DateTime maxValue)
            {
            }
            else
            {
                maxValue = DateTime.MaxValue;
            }

            seed ??= DateTime.Now.Millisecond;
            if (!_randomizers.TryGetValue(seed.Value, out var random))
            {
                random = new Random(seed.Value);
                _randomizers.Add(seed.Value, random);
            }

            for (var i = 0; i < count; ++i)
            {
                DateTime value;
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
    }
}