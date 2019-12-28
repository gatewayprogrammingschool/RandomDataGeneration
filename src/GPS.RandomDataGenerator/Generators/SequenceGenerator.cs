using System.Collections.Generic;
using GPS.RandomDataGenerator.Abstractions;

namespace GPS.RandomDataGenerator.Generators
{
    public class SequenceGenerator : IDataGenerator<int>
    {
        private int    _data;
        private object Lock { get; } = new object();

        private int Next
        {
            get
            {
                lock (Lock)
                {
                    return _data++;
                }
            }
        }

        public IEnumerable<int> Generate(int? seed, int count, params object[] options)
        {
            lock (Lock)
            {
                if (options.Length > 0 && options[0] is int start && _data < start) _data = start;
            }

            for (var i = 0; i < count; ++i) yield return Next;
        }
    }
}