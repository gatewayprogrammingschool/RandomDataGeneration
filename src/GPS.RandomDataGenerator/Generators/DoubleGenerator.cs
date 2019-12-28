using System;
using System.Collections.Generic;
using GPS.RandomDataGenerator.Abstractions;

namespace GPS.RandomDataGenerator.Generators
{
    public class DoubleGenerator : IDataGenerator<double>
    {
        private Dictionary<int, Random> Randomizers { get; } = new Dictionary<int, Random>();

        public IEnumerable<double> Generate(int? seed, int count, params object[] options)
        {
            if (options.Length > 0 && options[0] is double minValue)
            {
            }
            else
            {
                minValue = double.MinValue;
            }

            if (options.Length > 1 && options[1] is double maxValue)
            {
            }
            else
            {
                maxValue = double.MaxValue;
            }

            seed ??= DateTime.Now.Millisecond;
            if (!Randomizers.TryGetValue(seed.Value, out var random))
            {
                random = new Random(seed.Value);
                Randomizers.Add(seed.Value, random);
            }

            for (var i = 0; i < count; ++i)
            {
                var value = 0.0;
                if (maxValue - minValue >= double.PositiveInfinity ||
                    maxValue - minValue <= double.NegativeInfinity)
                {
                    var flip     = random.NextDouble() > 0.5 ? 1 : 0;
                    var rndValue = random.NextDouble();
                    value = rndValue * maxValue + minValue * flip;
                }
                else
                {
                    value = random.NextDouble() * (maxValue - minValue) + minValue;
                }

                yield return value;
            }
        }
    }
}