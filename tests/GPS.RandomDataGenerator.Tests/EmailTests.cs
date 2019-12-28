using System;
using System.Linq;
using GPS.RandomDataGenerator.Generators;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;
// ReSharper disable AccessToModifiedClosure

namespace GPS.RandomDataGenerator.Tests
{
    public class EmailTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public EmailTests(ITestOutputHelper testOutputHelper)
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
        public void GenerateEmailAddresses(int count)
        {
            var addresses = Provider.GetService<EmailGenerator>()?
                .Generate(0x0, count)?
                .ToList();

            Assert.NotNull(addresses);
            Assert.NotEmpty(addresses);
            Assert.DoesNotContain(addresses, s => s is null);
            Assert.Equal(count, addresses.Count);

            #if DEBUG
            _testOutputHelper.WriteLine($"Start Count: {count} ----------------------------");
            addresses.ForEach(address => _testOutputHelper.WriteLine(address));
            _testOutputHelper.WriteLine($"End Count: {count} ----------------------------");
            #endif
        }

        [Theory]
        [InlineData(0xA)]
        [InlineData(0x64)]
        [InlineData(0x3E8)]
        public void GenerateDifferentEmailAddresses(int count)
        {
            var baseAddresses = Provider.GetService<EmailGenerator>()?
                .Generate(0x0, count)?
                .ToList();

            Assert.NotNull(baseAddresses);
            Assert.NotEmpty(baseAddresses);
            Assert.DoesNotContain(baseAddresses, s => s is null);
            Assert.Equal(count, baseAddresses.Count);

            var addresses = Provider.GetService<EmailGenerator>()?
                .Generate(0x1, count)?
                .ToList();

            Assert.NotNull(addresses);
            Assert.NotEmpty(addresses);
            Assert.DoesNotContain(addresses, s => s is null);
            Assert.Equal(count, addresses.Count);
            var index = 0x0;

            // ReSharper disable once ImplicitlyCapturedClosure
            void TestAddress(string address) => Assert.NotSame(baseAddresses[index++], address);

            addresses.ForEach(TestAddress);

            #if DEBUG
            _testOutputHelper.WriteLine($"Start Count: {count} ----------------------------");
            index = 0x0;
            addresses.ForEach(address => _testOutputHelper.WriteLine($"{address} != {baseAddresses[index++]}"));
            _testOutputHelper.WriteLine($"End Count: {count} ----------------------------");
            #endif
        }
    }
}