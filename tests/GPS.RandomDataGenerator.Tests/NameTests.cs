using System;
using System.Linq;
using GPS.RandomDataGenerator.Generators;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

namespace GPS.RandomDataGenerator.Tests
{
    public class NameTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public NameTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            var service = new ServiceCollection();
            service.AddGenerators();
            Provider = service.BuildServiceProvider(true);
        }

        private IServiceProvider Provider { get; }

        [Theory]
        [InlineData(0xA)]
        [InlineData(0x64)]
        [InlineData(0x3E8)]
        public void GenerateNames(int count)
        {
            var names = Provider.GetService<NameGenerator>()?
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
    }
}