using System;
using System.Collections.Generic;
using System.Drawing;
using GPS.RandomDataGenerator.Data;
using GPS.RandomDataGenerator.Generators;
using Microsoft.Extensions.DependencyInjection;

namespace GPS.RandomDataGenerator
{
    public static class DependencyInjectionExtensions
    {
        private const string BaseData      = "GPS.RandomDataGenerator.BaseData";
        private const string SurnameData   = "SurNameData";
        private const string GivenNameData = "GivenNameData";
        private const string DomainData    = "DomainData";

        // ReSharper disable once UnusedMethodReturnValue.Global
        public static IServiceCollection AddGenerators(this IServiceCollection collection)
        {
            collection.AddTransient(provider =>
                                        new NameGenerator(provider, ReadSurNames(), ReadGivenNames()));

            collection.AddTransient(provider =>
                                        new SurNameGenerator(provider, ReadSurNames()));

            collection.AddTransient(provider =>
                                        new GivenNameGenerator(provider, ReadGivenNames()));

            collection.AddTransient(provider =>
                                        new EmailGenerator(provider, ReadDomains()));

            collection.AddTransient(provider =>
                                        new ColorNameGenerator(provider, ReadColors()));

            collection.AddSingleton<GuidGenerator, GuidGenerator>();
            collection.AddTransient<IntegerGenerator, IntegerGenerator>();
            collection.AddTransient<DoubleGenerator, DoubleGenerator>();
            collection.AddTransient<DecimalGenerator, DecimalGenerator>();
            collection.AddTransient<DateGenerator, DateGenerator>();
            collection.AddSingleton<SequenceGenerator, SequenceGenerator>();

            return collection;
        }

        private static IEnumerable<string> ReadSurNames()
        {
            return DataLoader.LoadData<string>(BaseData + "." + SurnameData, BaseData);
        }

        private static IEnumerable<string> ReadGivenNames()
        {
            return DataLoader.LoadData<string>(BaseData + "." + GivenNameData, BaseData);
        }

        private static IEnumerable<string> ReadDomains()
        {
            return DataLoader.LoadData<string>(BaseData + "." + DomainData, BaseData);
        }

        private static IEnumerable<string> ReadColors()
        {
            return Enum.GetNames(typeof(KnownColor));
        }
    }
}