using System;
using System.Collections.Generic;
using GPS.RandomDataGenerator.Generators;
using GPS.RandomDataGenerator.Abstractions;

namespace GPS.RandomDataGenerator.Options
{
    public class RangeOptions<TData, TResult> : RandomDataOptionsBase, IGeneratorOptions<TResult>
        where TResult : TData
        where TData : IComparable
    {
        public TData Min {get; set;}
        public TData Max {get;set;}

        public RangeOptions(int count, TData min = default, TData max = default) : base(count)        
        {
            if(Comparer<TData>.Default.Compare(min, max) <= 0)
            {
                Min = min;
                Max = max;
            }
            else
            {
                Min = max;
                Max = min;
            }
        }
 
        public IEnumerable<TResult> Generate(Random random)
        {
            var range = (min: Min, max: Max);

            return range switch
            {
                (int min, int max) value => (IEnumerable<TResult>)new IntegerGenerator().Generate(random, Count, min, max),
                (double min, double max) => (IEnumerable<TResult>)new DoubleGenerator().Generate(random, Count, min, max),
                (decimal min, decimal max) => (IEnumerable<TResult>)new DecimalGenerator().Generate(random, Count, min, max),
                (DateTime min, DateTime max) => (IEnumerable<TResult>)new DateGenerator().Generate(random, Count, min, max),
                _ => new List<TResult>()
            };
        }
    }
}