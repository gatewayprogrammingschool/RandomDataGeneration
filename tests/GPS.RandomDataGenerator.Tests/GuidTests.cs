using System;
using System.Collections.Generic;
using System.Linq;
using GPS.RandomDataGenerator.Generators;
using GPS.RandomDataGenerator.Extensions;
using GPS.RandomDataGenerator.Options;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

namespace GPS.RandomDataGenerator.Tests
{
    public class GuidTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public GuidTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            var service = new ServiceCollection();
            service.AddGenerators();
            Provider = service.BuildServiceProvider(true);
        }

        private IServiceProvider Provider { get; }

        [Theory]
        [InlineData(0x0, 0xA)]
        [InlineData(0x1, 0xA)]
        [InlineData(0x0, 0x2710)]
        public void GenerateGuid(int seed, int count)
        {
            var guids = Provider.GetService<GuidGenerator>()?
                .Generate(seed, count)?
                .ToList();

            Assert.NotNull(guids);
            Assert.NotEmpty(guids);
            Assert.DoesNotContain(guids, s => s == default);
            Assert.Equal(count, guids.Count);

            #if DEBUG
            _testOutputHelper.WriteLine($"Start Count: {count} ----------------------------");
            guids.ForEach(guid => _testOutputHelper.WriteLine($"{guid}"));
            _testOutputHelper.WriteLine($"End Count: {count} ----------------------------");
            #endif
        }

        [Theory]
        [InlineData(0x0, 0xA)]
        [InlineData(0x1, 0xA)]
        [InlineData(0x0, 0x2710)]
        public void GenerateGuidFromRandom(int seed, int count)
        {
            var random = new Random(seed);

            var guids = random.Generate<Guid, Guid>(Provider, count).ToList();

            Assert.NotNull(guids);
            Assert.NotEmpty(guids);
            Assert.DoesNotContain(guids, s => s == default);
            Assert.Equal(count, guids.Count);

            #if DEBUG
            _testOutputHelper.WriteLine($"Start Count: {count} ----------------------------");
            guids.ForEach(guid => _testOutputHelper.WriteLine($"{guid}"));
            _testOutputHelper.WriteLine($"End Count: {count} ----------------------------");
            #endif
        }
    
        [Theory]
        [InlineData(0x0, 0xA)]
        [InlineData(0x1, 0xA)]
        [InlineData(0x0, 0x2710)]
        public void GenerateGuidNext(int seed, int count)
        {
            var guids = new List<Guid>();
            var random = new Random(seed);

            for(var i = 0; i < count; ++i)
            {
                var guid = random.Next<Guid>(Provider);

                guids.Add(guid);
            }

            Assert.NotNull(guids);
            Assert.NotEmpty(guids);
            Assert.DoesNotContain(guids, s => s == default);
            Assert.Equal(count, guids.Count);

            #if DEBUG
            _testOutputHelper.WriteLine($"Start Count: {count} ----------------------------");
            guids.ForEach(guid => _testOutputHelper.WriteLine($"{guid}"));
            _testOutputHelper.WriteLine($"End Count: {count} ----------------------------");
            #endif
        }
}
}