using System;
using System.Collections.Generic;
using GPS.RandomDataGenerator.Generators;
using GPS.RandomDataGenerator.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace GPS.RandomDataGenerator.Options
{
    public class Options<TData, TResult> : RandomDataOptionsBase, IGeneratorOptions<TResult>
    {
        public Options(IServiceProvider serviceProvider, int count) : base(count)
        {
            ServiceProvider = serviceProvider;
        }

        public Options(IServiceProvider serviceProvider, int count, TData gs) : base(count)
        {
            ServiceProvider = serviceProvider;

            if(gs is GenerateStrings s)
            {
                StringType = s;
            }
            else
            {
                throw new ArgumentException("This constructor only valid for TData = GenerateStrings.");
            }
        }

        public IServiceProvider ServiceProvider { get; }
        public GenerateStrings StringType { get; internal set; }

        public IEnumerable<TResult> Generate(Random random)
        {
            return default(TData) switch
            {
                System.Drawing.Color color => (IEnumerable<TResult>)ServiceProvider.GetService<ColorNameGenerator>().Generate(random, Count),
                Guid guid => (IEnumerable<TResult>)ServiceProvider.GetService<GuidGenerator>().Generate(random, Count),
                GenerateStrings gs when StringType == GenerateStrings.SurName => (IEnumerable<TResult>)ServiceProvider.GetService<SurNameGenerator>().Generate(random, Count),
                GenerateStrings gs when StringType == GenerateStrings.GivenName => (IEnumerable<TResult>)ServiceProvider.GetService<GivenNameGenerator>().Generate(random, Count),
                GenerateStrings gs when StringType == GenerateStrings.FullName => (IEnumerable<TResult>)ServiceProvider.GetService<NameGenerator>().Generate(random, Count),
                GenerateStrings gs when StringType == GenerateStrings.EmailAddress => (IEnumerable<TResult>)ServiceProvider.GetService<EmailGenerator>().Generate(random, Count),
                _ => new List<TResult>()
            };
        }
    }
}