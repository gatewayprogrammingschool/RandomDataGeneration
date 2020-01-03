using System.Collections.Generic;
using GPS.RandomDataGenerator.Abstractions;

namespace GPS.RandomDataGenerator.Generators
{
    public class SequenceGenerator : IDataGenerator<int>, IResetable
    {
        private int _data;
        private object Lock { get; } = new object();

        public int GetNext(int start)
        {
            lock (Lock)
            {
                if(start > _data) _data = start;

                return _data++;
            }
        }

        public int GetNext()
        {
            lock(Lock)
            {
                return _data++;
            }
        }

        private bool NotFinished(int count)
        {
            lock (Lock)
            {
                return _data < count;
            }
        }

        public IEnumerable<int> Generate(int? seed, int count, params object[] options)
        {
            var results = new int[count];

            lock (Lock)
            {
                if (options.Length > 0 && options[0] is int start && _data < start) _data = start;

                for(var i = 0; i < count; ++i)
                {
                    results[i] = GetNext();
                }
            }
            
            return results;
        }
 
        public void Reset(int seed)
        {
            lock(Lock)
            {
                _data = 0;
            }
        }
    }
}