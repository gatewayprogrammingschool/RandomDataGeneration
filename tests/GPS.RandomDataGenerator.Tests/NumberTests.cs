using System;
using System.Collections.Generic;
using GPS.RandomDataGenerator.Extensions;
using GPS.RandomDataGenerator.Options;
using System.Linq;
using GPS.RandomDataGenerator.Generators;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

namespace GPS.RandomDataGenerator.Tests
{
    public class NumberTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public NumberTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            var service = new ServiceCollection();
            service.AddGenerators();
            Provider = service.BuildServiceProvider(true);
        }

        private IServiceProvider Provider { get; }

        [Theory]
        [InlineData(0x0, 0xA, 0x0, 0x19)]
        [InlineData(0x1, 0xA, 0x0, 0x19)]
        [InlineData(0x0, 0x64, -0x3E8, 0x3E8)]
        [InlineData(0x0, 0x2710, int.MinValue, int.MaxValue)]
        public void GenerateIntegers(int seed, int count, int min, int max)
        {
            var ints = Provider.GetService<IntegerGenerator>()?
                .Generate(seed, count, min, max)?
                .ToList();

            Assert.NotNull(ints);
            Assert.NotEmpty(ints);
            Assert.Equal(count, ints.Count);
            Assert.True(Math.Max(max, ints.Max()) == max);
            Assert.True(Math.Min(min, ints.Min()) == min);
            #if DEBUG
            _testOutputHelper.WriteLine($"Start Count: {count} ----------------------------");
            ints.ForEach(i => _testOutputHelper.WriteLine($"{i}"));
            _testOutputHelper.WriteLine($"End Count: {count} ----------------------------");
            #endif
        }

        [Theory]
        [InlineData(0x0, 0xA, 0x0, 0x19)]
        [InlineData(0x1, 0xA, 0x0, 0x19)]
        [InlineData(0x0, 0x64, -0x3E8, 0x3E8)]
        [InlineData(0x0, 0x2710, int.MinValue, int.MaxValue)]
        public void GenerateIntegersFromRandom(int seed, int count, int min, int max)
        {
            var random = new Random(seed);

            var ints = random.Generate(count, min, max).ToList();

            Assert.NotNull(ints);
            Assert.NotEmpty(ints);
            Assert.Equal(count, ints.Count);
            Assert.True(Math.Max(max, ints.Max()) == max);
            Assert.True(Math.Min(min, ints.Min()) == min);
            #if DEBUG
            _testOutputHelper.WriteLine($"Start Count: {count} ----------------------------");
            ints.ForEach(i => _testOutputHelper.WriteLine($"{i}"));
            _testOutputHelper.WriteLine($"End Count: {count} ----------------------------");
            #endif
        }
        
        [Theory]
        [InlineData(0x0, 0xA, 0x0, 0x19)]
        [InlineData(0x1, 0xA, 0x0, 0x19)]
        [InlineData(0x0, 0x64, -0x3E8, 0x3E8)]
        [InlineData(0x0, 0x2710, int.MinValue, int.MaxValue)]
        public void GenerateIntegersFromOptions(int seed, int count, int min, int max)
        {
            var options = new RangeOptions<int, int>(count, min, max);
            var random = new Random(seed);

            var ints = random.Generate(options).ToList();

            Assert.NotNull(ints);
            Assert.NotEmpty(ints);
            Assert.Equal(options.Count, ints.Count);
            Assert.True(Math.Max(options.Max, ints.Max()) == options.Max);
            Assert.True(Math.Min(options.Min, ints.Min()) == options.Min);
            #if DEBUG
            _testOutputHelper.WriteLine($"Start Count: {options.Count} ----------------------------");
            ints.ForEach(i => _testOutputHelper.WriteLine($"{i}"));
            _testOutputHelper.WriteLine($"End Count: {options.Count} ----------------------------");
            #endif
        }
        
        [Theory]
        [InlineData(0x0, 0xA, 0x0u, 0x19u)]
        [InlineData(0x1, 0xA, 0x0u, 0x19u)]
        [InlineData(0x0, 0x64, 0x0u, 0x3E8u)]
        [InlineData(0x0, 0x2710, uint.MinValue, uint.MaxValue)]
        public void GenerateUnsignedIntegersFromOptions(int seed, int count, uint min, uint max)
        {
            var options = new RangeOptions<uint, uint>(count, min, max);
            var random = new Random(seed);

            var uints = random.Generate(options).ToList();

            Assert.NotNull(uints);
            Assert.NotEmpty(uints);
            Assert.Equal(options.Count, uints.Count);
            Assert.True(Math.Max(options.Max, uints.Max()) == options.Max);
            Assert.True(Math.Min(options.Min, uints.Min()) == options.Min);
            #if DEBUG
            _testOutputHelper.WriteLine($"Start Count: {options.Count} ----------------------------");
            uints.ForEach(i => _testOutputHelper.WriteLine($"{i}"));
            _testOutputHelper.WriteLine($"End Count: {options.Count} ----------------------------");
            #endif
        }
        
        [Theory]
        [InlineData(0x0, 0xA, 0x0, 0x19)]
        [InlineData(0x1, 0xA, 0x0, 0x19)]
        [InlineData(0x0, 0x64, 0x0, 0xFF)]
        [InlineData(0x0, 0x2710, byte.MinValue, byte.MaxValue)]
        public void GenerateBytesFromOptions(int seed, int count, byte min, byte max)
        {
            var options = new RangeOptions<byte, byte>(count, min, max);
            var random = new Random(seed);

            var bytes = random.Generate(options).ToList();

            Assert.NotNull(bytes);
            Assert.NotEmpty(bytes);
            Assert.Equal(options.Count, bytes.Count);
            Assert.True(Math.Max(options.Max, bytes.Max()) == options.Max);
            Assert.True(Math.Min(options.Min, bytes.Min()) == options.Min);
            #if DEBUG
            _testOutputHelper.WriteLine($"Start Count: {options.Count} ----------------------------");
            bytes.ForEach(i => _testOutputHelper.WriteLine($"{i}"));
            _testOutputHelper.WriteLine($"End Count: {options.Count} ----------------------------");
            #endif
        }

        [Theory]
        [InlineData(0x0, 0xA, 0x0, 0x1)]
        [InlineData(0x1, 0xA, 0x0, 0x1)]
        [InlineData(0x0, 0x64, -1000.0, 1000.0)]
        [InlineData(0x0, 0x2710, double.MinValue, double.MaxValue)]
        public void GenerateDoubles(int seed, int count, double min, double max)
        {
            var doubles = Provider.GetService<DoubleGenerator>()?
                .Generate(seed, count, min, max)?
                .ToList();

            Assert.NotNull(doubles);
            Assert.NotEmpty(doubles);
            Assert.Equal(count, doubles.Count);
            Assert.Equal(max, Math.Max(max, doubles.Max()));
            Assert.Equal(min, Math.Min(min, doubles.Min()));
            #if DEBUG
            _testOutputHelper.WriteLine($"Start Count: {count} ----------------------------");
            doubles.ForEach(value => _testOutputHelper.WriteLine($"{value}"));
            _testOutputHelper.WriteLine($"End Count: {count} ----------------------------");
            #endif
        }

        [Theory]
        [InlineData(0x0, 0xA, 0x0, 0x1)]
        [InlineData(0x1, 0xA, 0x0, 0x1)]
        [InlineData(0x0, 0x64, -1000.0, 1000.0)]
        [InlineData(0x0, 0x2710, double.MinValue, double.MaxValue)]
        public void GenerateDoublesFromRandom(int seed, int count, double min, double max)
        {
            var random = new Random(seed);

            var doubles = random.Generate(count, min, max).ToList();

            Assert.NotNull(doubles);
            Assert.NotEmpty(doubles);
            Assert.Equal(count, doubles.Count);
            Assert.Equal(max, Math.Max(max, doubles.Max()));
            Assert.Equal(min, Math.Min(min, doubles.Min()));
            #if DEBUG
            _testOutputHelper.WriteLine($"Start Count: {count} ----------------------------");
            doubles.ForEach(value => _testOutputHelper.WriteLine($"{value}"));
            _testOutputHelper.WriteLine($"End Count: {count} ----------------------------");
            #endif
        }

        [Theory]
        [InlineData(0x0, 0xA, 0x0, 0x1)]
        [InlineData(0x1, 0xA, 0x0, 0x1)]
        [InlineData(0x0, 0x64, -1000.0, 1000.0)]
        [InlineData(0x0, 0x2710, double.MinValue, double.MaxValue)]
        public void GenerateDoublesFromOptions(int seed, int count, double min, double max)
        {
            var options = new RangeOptions<double, double>(count, min, max);
            var random = new Random(seed);

            var doubles = random.Generate(options).ToList();

            Assert.NotNull(doubles);
            Assert.NotEmpty(doubles);
            Assert.Equal(count, doubles.Count);
            Assert.Equal(max, Math.Max(max, doubles.Max()));
            Assert.Equal(min, Math.Min(min, doubles.Min()));
            #if DEBUG
            _testOutputHelper.WriteLine($"Start Count: {count} ----------------------------");
            doubles.ForEach(value => _testOutputHelper.WriteLine($"{value}"));
            _testOutputHelper.WriteLine($"End Count: {count} ----------------------------");
            #endif
        }

        [Theory]
        [InlineData(10, 0)]
        [InlineData(20, 100)]
        public void GenerateSequences(int count, int start)
        {
            var sequence = Provider.GetService<SequenceGenerator>()?
                .Generate(0, count, start)?
                .ToList();

            Assert.NotNull(sequence);
            Assert.NotEmpty(sequence);
            Assert.Equal(count, sequence.Count);
            Assert.Equal(start + count - 1, sequence.Max());
            Assert.Equal(start, sequence.Min());
            #if DEBUG
            _testOutputHelper.WriteLine($"Start Count: {count} ----------------------------");
            sequence.ForEach(value => _testOutputHelper.WriteLine($"{value}"));
            _testOutputHelper.WriteLine($"End Count: {count} ----------------------------");
            #endif
        }

        [Theory]
        [InlineData(10, 0)]
        [InlineData(20, 100)]
        public void GenerateSequencesNext(int count, int start)
        {
            var sequence = new int[count];
            var sequenceGenerator = Provider.GetService<SequenceGenerator>();

            for(var i = 0; i < count; ++i)
            {
                sequence[i] = sequenceGenerator.GetNext(start);
            }

            Assert.NotNull(sequence);
            Assert.NotEmpty(sequence);
            Assert.Equal(count, sequence.Length);
            Assert.Equal(start + count - 1, sequence.Max());
            Assert.Equal(start, sequence.Min());
            #if DEBUG
            _testOutputHelper.WriteLine($"Start Count: {count} ----------------------------");
            sequence.ToList().ForEach(value => _testOutputHelper.WriteLine($"{value}"));
            _testOutputHelper.WriteLine($"End Count: {count} ----------------------------");
            #endif
        }


        [Theory]
        [MemberData(nameof(DateTimeOffsetDataSource.TestData), MemberType = typeof(DateTimeOffsetDataSource))]
        public void GenerateDateOffsets(int seed, int count, DateTimeOffset min, DateTimeOffset max)
        {
            var dates = Provider.GetService<DateOffsetGenerator>()?
                .Generate(seed, count, min, max)?
                .ToList();

            Assert.NotNull(dates);
            Assert.NotEmpty(dates);
            Assert.Equal(count, dates.Count);
            Assert.Equal(max.Ticks, Math.Max(max.Ticks, dates.Max().Ticks));
            Assert.Equal(min.Ticks, Math.Min(min.Ticks, dates.Min().Ticks));
            #if DEBUG
            _testOutputHelper.WriteLine($"Start Count: {count} ----------------------------");
            dates.ForEach(value => _testOutputHelper.WriteLine($"{value}"));
            _testOutputHelper.WriteLine($"End Count: {count} ----------------------------");
            #endif
        }

        [Theory]
        [MemberData(nameof(DateTimeDataSource.TestData), MemberType = typeof(DateTimeDataSource))]
        public void GenerateDates(int seed, int count, DateTime min, DateTime max)
        {
            var dates = Provider.GetService<DateGenerator>()?
                .Generate(seed, count, min, max)?
                .ToList();

            Assert.NotNull(dates);
            Assert.NotEmpty(dates);
            Assert.Equal(count, dates.Count);
            Assert.Equal(max.Ticks, Math.Max(max.Ticks, dates.Max().Ticks));
            Assert.Equal(min.Ticks, Math.Min(min.Ticks, dates.Min().Ticks));
            #if DEBUG
            _testOutputHelper.WriteLine($"Start Count: {count} ----------------------------");
            dates.ForEach(value => _testOutputHelper.WriteLine($"{value}"));
            _testOutputHelper.WriteLine($"End Count: {count} ----------------------------");
            #endif
        }
        
        [Theory]
        [MemberData(nameof(DateTimeOffsetDataSource.TestData), MemberType = typeof(DateTimeOffsetDataSource))]
        public void GenerateDateOffsetsFromRandom(int seed, int count, DateTimeOffset min, DateTimeOffset max)
        {
            var random = new Random(seed);

            var dates = random.Generate(count, min, max).ToList();

            Assert.NotNull(dates);
            Assert.NotEmpty(dates);
            Assert.Equal(count, dates.Count);
            Assert.Equal(max.Ticks, Math.Max(max.Ticks, dates.Max().Ticks));
            Assert.Equal(min.Ticks, Math.Min(min.Ticks, dates.Min().Ticks));
            #if DEBUG
            _testOutputHelper.WriteLine($"Start Count: {count} ----------------------------");
            dates.ForEach(value => _testOutputHelper.WriteLine($"{value}"));
            _testOutputHelper.WriteLine($"End Count: {count} ----------------------------");
            #endif
        }
        
        [Theory]
        [MemberData(nameof(DateTimeDataSource.TestData), MemberType = typeof(DateTimeDataSource))]
        public void GenerateDatesFromRandom(int seed, int count, DateTime min, DateTime max)
        {
            var random = new Random(seed);

            var dates = random.Generate(count, min, max).ToList();

            Assert.NotNull(dates);
            Assert.NotEmpty(dates);
            Assert.Equal(count, dates.Count);
            Assert.Equal(max.Ticks, Math.Max(max.Ticks, dates.Max().Ticks));
            Assert.Equal(min.Ticks, Math.Min(min.Ticks, dates.Min().Ticks));
            #if DEBUG
            _testOutputHelper.WriteLine($"Start Count: {count} ----------------------------");
            dates.ForEach(value => _testOutputHelper.WriteLine($"{value}"));
            _testOutputHelper.WriteLine($"End Count: {count} ----------------------------");
            #endif
        }

        [Theory]
        [MemberData(nameof(DateTimeOffsetDataSource.TestData), MemberType = typeof(DateTimeOffsetDataSource))]
        public void GenerateDateOffsetsFromOptions(int seed, int count, DateTimeOffset min, DateTimeOffset max)
        {
            var options = new RangeOptions<DateTimeOffset, DateTimeOffset>(count, min, max);
            var random = new Random(seed);

            var dates = random.Generate(options).ToList();

            Assert.NotNull(dates);
            Assert.NotEmpty(dates);
            Assert.Equal(count, dates.Count);
            Assert.Equal(max.Ticks, Math.Max(max.Ticks, dates.Max().Ticks));
            Assert.Equal(min.Ticks, Math.Min(min.Ticks, dates.Min().Ticks));
            #if DEBUG
            _testOutputHelper.WriteLine($"Start Count: {count} ----------------------------");
            dates.ForEach(value => _testOutputHelper.WriteLine($"{value}"));
            _testOutputHelper.WriteLine($"End Count: {count} ----------------------------");
            #endif
        }

        [Theory]
        [MemberData(nameof(DateTimeDataSource.TestData), MemberType = typeof(DateTimeDataSource))]
        public void GenerateDatesFromOptions(int seed, int count, DateTime min, DateTime max)
        {
            var options = new RangeOptions<DateTime, DateTime>(count, min, max);
            var random = new Random(seed);

            var dates = random.Generate(options).ToList();

            Assert.NotNull(dates);
            Assert.NotEmpty(dates);
            Assert.Equal(count, dates.Count);
            Assert.Equal(max.Ticks, Math.Max(max.Ticks, dates.Max().Ticks));
            Assert.Equal(min.Ticks, Math.Min(min.Ticks, dates.Min().Ticks));
            #if DEBUG
            _testOutputHelper.WriteLine($"Start Count: {count} ----------------------------");
            dates.ForEach(value => _testOutputHelper.WriteLine($"{value}"));
            _testOutputHelper.WriteLine($"End Count: {count} ----------------------------");
            #endif
        }

        [Theory]
        [MemberData(nameof(DecimalDataSource.TestData), MemberType = typeof(DecimalDataSource))]
        public void GenerateDecimals(int seed, int count, decimal min, decimal max)
        {
            var decimals = Provider.GetService<DecimalGenerator>()?
                .Generate(seed, count, min, max)?
                .ToList();

            Assert.NotNull(decimals);
            Assert.NotEmpty(decimals);
            Assert.Equal(count, decimals.Count);
            Assert.Equal(max, Math.Max(max, decimals.Max()));
            Assert.Equal(min, Math.Min(min, decimals.Min()));
            #if DEBUG
            _testOutputHelper.WriteLine($"Start Count: {count} ----------------------------");
            decimals.ForEach(value => _testOutputHelper.WriteLine($"{value}"));
            _testOutputHelper.WriteLine($"End Count: {count} ----------------------------");
            #endif
        }

        [Theory]
        [MemberData(nameof(DecimalDataSource.TestData), MemberType = typeof(DecimalDataSource))]
        public void GenerateDecimalsFromRandom(int seed, int count, decimal min, decimal max)
        {
            var random = new Random(seed);

            var decimals = random.Generate(count, min, max).ToList();

            Assert.NotNull(decimals);
            Assert.NotEmpty(decimals);
            Assert.Equal(count, decimals.Count);
            Assert.Equal(max, Math.Max(max, decimals.Max()));
            Assert.Equal(min, Math.Min(min, decimals.Min()));
            #if DEBUG
            _testOutputHelper.WriteLine($"Start Count: {count} ----------------------------");
            decimals.ForEach(value => _testOutputHelper.WriteLine($"{value}"));
            _testOutputHelper.WriteLine($"End Count: {count} ----------------------------");
            #endif
        }

        [Theory]
        [MemberData(nameof(DecimalDataSource.TestData), MemberType = typeof(DecimalDataSource))]
        public void GenerateDecimalsFromOptions(int seed, int count, decimal min, decimal max)
        {
            var options = new RangeOptions<decimal, decimal>(count, min, max);
            var random = new Random(seed);

            var decimals = random.Generate(options).ToList();


            Assert.NotNull(decimals);
            Assert.NotEmpty(decimals);
            Assert.Equal(count, decimals.Count);
            Assert.Equal(max, Math.Max(max, decimals.Max()));
            Assert.Equal(min, Math.Min(min, decimals.Min()));
            #if DEBUG
            _testOutputHelper.WriteLine($"Start Count: {count} ----------------------------");
            decimals.ForEach(value => _testOutputHelper.WriteLine($"{value}"));
            _testOutputHelper.WriteLine($"End Count: {count} ----------------------------");
            #endif
        }
    }

    public static class DecimalDataSource
    {
        private static readonly List<object[]> Data =
            new List<object[]>
            {
                new object[] {0x0, 0xA, 0m, 1m},
                new object[] {0x1, 0xA, 0m, 1m},
                new object[] {0x0, 0x64, -1000.0m, 1000.0m},
                new object[] {0x0, 0x186A0, decimal.MinValue, decimal.MaxValue}
            };

        public static IEnumerable<object[]> TestData => Data;
    }

    public static class DateTimeDataSource
    {
        private static readonly List<object[]> Data =
            new List<object[]>
            {
                new object[] {0x0, 0xA, new DateTime(0x7D0, 0x1, 0x1), new DateTime(0x7D0, 0xC, 0x1F)},
                new object[] {0x1, 0xA, new DateTime(0x7D0, 0x1, 0x1), new DateTime(0x7D0, 0xC, 0x1F)},
                new object[] {0x0, 0x64, new DateTime(0x1, 0x1, 0x1), new DateTime(0x7E3, 0xC, 0x1F)},
                new object[] {0x0, 0x186A0, DateTime.MinValue, DateTime.MaxValue}
            };

        public static IEnumerable<object[]> TestData => Data;
    }

    public static class DateTimeOffsetDataSource
    {
        private static readonly List<object[]> Data =
            new List<object[]>
            {
                new object[] {0x0, 0xA, new DateTimeOffset(new DateTime(0x7D0, 0x1, 0x1)), new DateTimeOffset(new DateTime(0x7D0, 0xC, 0x1F))},
                new object[] {0x1, 0xA, new DateTimeOffset(new DateTime(0x7D0, 0x1, 0x1)), new DateTimeOffset(new DateTime(0x7D0, 0xC, 0x1F))},
                new object[] {0x0, 0x64, new DateTimeOffset(new DateTime(0x1, 0x1, 0x1)), new DateTimeOffset(new DateTime(0x7E3, 0xC, 0x1F))},
                new object[] {0x0, 0x186A0, DateTimeOffset.MinValue, DateTimeOffset.MaxValue}
            };

        public static IEnumerable<object[]> TestData => Data;
    }}