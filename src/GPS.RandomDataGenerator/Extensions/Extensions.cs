using System;
using System.Collections.Generic;
using System.Linq;
using GPS.RandomDataGenerator.Options;
using GPS.RandomDataGenerator.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace GPS.RandomDataGenerator.Extensions
{
    public static class Extensions
    {
        public static IEnumerable<TData> Generate<TData>(this Random random, RangeOptions<TData, TData> options)
            where TData : IComparable
        {
            return options.Generate(random);
        }

        public static IEnumerable<TData> Generate<TData>(this Random random, int count, TData min, TData max)
            where TData : IComparable
        {
            var options = new RangeOptions<TData, TData>(count, min, max);

            return options.Generate(random);
        }

        public static IEnumerable<TResult> Generate<TData, TResult>(this Random random, Options<TData, TResult> options)
        {
            return options.Generate(random);
        }

        public static IEnumerable<TResult> Generate<TData, TResult>(this Random random, IServiceProvider serviceProvider, int count)
        {
            var options = new Options<TData, TResult>(serviceProvider, count);

            return options.Generate(random);
        }

        public static IEnumerable<string> Generate(
            this Random random
            , IServiceProvider serviceProvider
            , int count
            , GenerateStrings stringType)
        {
            var options = new Options<GenerateStrings, string>(serviceProvider, count) { StringType = stringType };

            return options.Generate(random);
        }

        public static TData Next<TData>(this Random random, TData min, TData max)
            where TData : IComparable
        {
            return From<TData>(min, max).Generate(random).First();

            IGeneratorOptions<TResult> From<TResult>(TData min = default, TData max = default)
                where TResult : TData
            {
                return new RangeOptions<TData, TResult>(1, min, max);
            }
        }

        public static string Next(this Random random, IServiceProvider provider, GenerateStrings gs)
        {
            return From(provider, gs).Generate(random).First();

            IGeneratorOptions<string> From(IServiceProvider provider, GenerateStrings gs)
            {
                return new Options<GenerateStrings, string>(provider, 1, gs);
            }
        }
            
        public static TData Next<TData>(this Random random, IServiceProvider provider)
        {
            return From().Generate(random).First();

            IGeneratorOptions<TData> From()
            {
                return new Options<TData, TData>(provider, 1);
            }
        }
}
}