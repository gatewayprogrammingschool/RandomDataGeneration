using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GPS.RandomDataGenerator.Abstractions;

namespace GPS.RandomDataGenerator.Generators
{
    public class GuidGenerator : IDataGenerator<Guid>, IResetable
    {
        private Dictionary<int, Random> Randomizers { get; } = new Dictionary<int, Random>();

        public IEnumerable<Guid> Generate(Random random, int count)
        {
            var seed = random.GetHashCode();

            if(!Randomizers.ContainsKey(seed)) Randomizers.Add(seed, random);

            return Generate(seed, count);
        }

        public IEnumerable<Guid> Generate(int? seed, int count, params object[] options)
        {
            var random = new Random(seed ?? DateTime.Now.Millisecond);
            if (Randomizers.ContainsKey(seed.Value))
                random = Randomizers[seed.Value];
            else
                Randomizers.TryAdd(seed.Value, random);

            for (var i = 0; i < count; ++i)
            {
                var bytes = Guid.Empty.ToByteArray();
                random.NextBytes(bytes);
                var result = MemoryMarshal.Cast<byte, Guid>(bytes.AsSpan())[0];

                yield return result;
            }
        }
 
        public void Reset(int seed)
        {
            if(Randomizers.ContainsKey(seed)) Randomizers.Remove(seed);
        }
    }
}