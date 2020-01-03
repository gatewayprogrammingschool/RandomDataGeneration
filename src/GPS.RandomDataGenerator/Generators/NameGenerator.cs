using System;
using System.Collections.Generic;
using System.Linq;
using GPS.RandomDataGenerator.Abstractions;
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace GPS.RandomDataGenerator.Generators
{
    public class NameGenerator : IDataGenerator<string>, IResetable
    {
        private Dictionary<int, Random> SurNameRandomizers { get; } = new Dictionary<int, Random>();
        private Dictionary<int, Random> GivenNameRandomizers { get; } = new Dictionary<int, Random>();

        public NameGenerator(IServiceProvider provider
            , IEnumerable<string> surnames
            , IEnumerable<string> givenNames)
        {
            Provider = provider;
            SurNames = surnames.ToArray();
            GivenNames = givenNames.ToArray();
        }

        private IServiceProvider Provider   { get; }
        private string[]         SurNames   { get; }
        private string[]         GivenNames { get; }

        public IEnumerable<string> Generate(Random random, int count)
        {
            var seed = random.GetHashCode();

            if(!SurNameRandomizers.ContainsKey(seed)) SurNameRandomizers.Add(seed, random);
            if(!GivenNameRandomizers.ContainsKey(seed)) GivenNameRandomizers.Add(seed, random);

            for (var i = 0; i < count; ++i) 
            {
                yield return
                    $"{GivenNames[random.Next(0, GivenNames.Length - 1)]} {SurNames[random.Next(0, SurNames.Length - 1)]}";
            }
        }

        public IEnumerable<string> Generate(int? seed, int count, params object[] options)
        {
            seed ??= DateTime.Now.Millisecond;
            if (!SurNameRandomizers.TryGetValue(seed.Value, out var surNameRandom))
            {
                surNameRandom = new Random(seed.Value);
                SurNameRandomizers.Add(seed.Value, surNameRandom);
            }

            if (!GivenNameRandomizers.TryGetValue(seed.Value, out var givenNameRandom))
            {
                givenNameRandom = new Random(seed.Value);
                GivenNameRandomizers.Add(seed.Value, givenNameRandom);
            }

            for (var i = 0; i < count; ++i) 
            {
                yield return
                    $"{GivenNames[givenNameRandom.Next(0, GivenNames.Length - 1)]} {SurNames[surNameRandom.Next(0, SurNames.Length - 1)]}";
            }
        }
 
        public void Reset(int seed)
        {
            if(SurNameRandomizers.ContainsKey(seed)) SurNameRandomizers.Remove(seed);
            if(GivenNameRandomizers.ContainsKey(seed)) GivenNameRandomizers.Remove(seed);
        }
    }
}