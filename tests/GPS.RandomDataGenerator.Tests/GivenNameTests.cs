using System;
using System.Linq;
using GPS.RandomDataGenerator.Generators;
using GPS.RandomDataGenerator.Extensions;
using GPS.RandomDataGenerator.Options;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;
using System.Collections.Generic;

namespace GPS.RandomDataGenerator.Tests
{
    public class GivenNameTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public GivenNameTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            var service = new ServiceCollection();
            service.AddGenerators();
            Provider = service.BuildServiceProvider(true);
        }

        private IServiceProvider Provider { get; }

        [Theory]
        [InlineData(0xA)]
        //[InlineData(0x64)]
        //[InlineData(0x3E8)]
        public void GenerateGivenNames(int count)
        {
            var names = Provider.GetService<GivenNameGenerator>()?
                .Generate(0x0, count)?
                .ToList();

            Assert.NotNull(names);
            Assert.NotEmpty(names);
            Assert.DoesNotContain(names, s => s is null);
            Assert.Equal(count, names.Count);

            #if DEBUG
            _testOutputHelper.WriteLine($"Start Count: {count} ----------------------------");
            names.ForEach(name => _testOutputHelper.WriteLine(name));
            _testOutputHelper.WriteLine($"End Count: {count} ----------------------------");
            #endif
        }

        [Theory]
        [InlineData(0xA)]
        //[InlineData(0x64)]
        //[InlineData(0x3E8)]
        public void GenerateGivenNamesFromRandom(int count)
        {
            var random = new Random(0x0);

            var names = random.Generate(Provider, count, GenerateStrings.GivenName).ToList();

            Assert.NotNull(names);
            Assert.NotEmpty(names);
            Assert.DoesNotContain(names, s => s is null);
            Assert.Equal(count, names.Count);

            #if DEBUG
            _testOutputHelper.WriteLine($"Start Count: {count} ----------------------------");
            names.ForEach(name => _testOutputHelper.WriteLine(name));
            _testOutputHelper.WriteLine($"End Count: {count} ----------------------------");
            #endif
        }

        [Theory]
        [InlineData(0xA)]
        //[InlineData(0x64)]
        //[InlineData(0x3E8)]
        public void GenerateGivenNamesNext(int count)
        {
            var random = new Random(0x0);

            var names = new List<string>();

            for(var i = 0; i < count; ++i)
            {
                var name = random.Next(Provider, GenerateStrings.GivenName);

                names.Add(name);
            }

            Assert.NotNull(names);
            Assert.NotEmpty(names);
            Assert.DoesNotContain(names, s => s is null);
            Assert.Equal(count, names.Count);

            #if DEBUG
            _testOutputHelper.WriteLine($"Start Count: {count} ----------------------------");
            names.ForEach(name => _testOutputHelper.WriteLine(name));
            _testOutputHelper.WriteLine($"End Count: {count} ----------------------------");
            #endif
        }
    }
}