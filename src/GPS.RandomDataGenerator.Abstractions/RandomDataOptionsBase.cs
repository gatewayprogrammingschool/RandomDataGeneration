using System;

namespace GPS.RandomDataGenerator.Abstractions
{
    public abstract class RandomDataOptionsBase
    {
        public int Count { get; set; }

        protected RandomDataOptionsBase(int count)
        {
            if(count < 0) throw new ArgumentOutOfRangeException($"{nameof(count)} must be greater or equal to zero.");
            Count = count;
        }
    }
}