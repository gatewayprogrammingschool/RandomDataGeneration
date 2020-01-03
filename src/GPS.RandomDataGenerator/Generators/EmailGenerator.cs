using System;
using System.Collections.Generic;
using System.Linq;
using GPS.RandomDataGenerator.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace GPS.RandomDataGenerator.Generators
{
    public class EmailGenerator : IDataGenerator<string>, IResetable
    {
        private Dictionary<int, Random> Randomizers { get; } = new Dictionary<int, Random>();

        public EmailGenerator(IServiceProvider provider
            , IEnumerable<string> domains)
        {
            Provider = provider;
            Domains = domains.ToArray();
        }

        private IServiceProvider Provider { get; }
        private string[]         Domains  { get; }

        public IEnumerable<string> Generate(Random random, int count)
        {
            var seed = random.GetHashCode();

            if(!Randomizers.ContainsKey(seed)) Randomizers.Add(seed, random);

            var names = Provider.GetService<NameGenerator>().Generate(random, count);

            foreach (var name in names)
                yield return
                    $"{name.Replace(' ', '.').ToLowerInvariant()}@{Domains[random.Next(0, Domains.Length - 1)].ToLowerInvariant()}";
        }

        public IEnumerable<string> Generate(int? seed, int count, params object[] options)
        {
            seed ??= DateTime.Now.Millisecond;
            if (!Randomizers.TryGetValue(seed.Value, out var random))
            {
                random = new Random(seed.Value);
                Randomizers.Add(seed.Value, random);
            }

            var names = Provider.GetService<NameGenerator>().Generate(seed, count, options);

            foreach (var name in names)
                yield return
                    $"{name.Replace(' ', '.').ToLowerInvariant()}@{Domains[random.Next(0, Domains.Length - 1)].ToLowerInvariant()}";
        }
 
        public void Reset(int seed)
        {
            if(Randomizers.ContainsKey(seed)) Randomizers.Remove(seed);
        }
    }
}