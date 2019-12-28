using System;
using System.Linq;
using System.Reflection;
using GPS.RandomDataGenerator.RecordGenerators;
// ReSharper disable ClassNeverInstantiated.Global

namespace GPS.RandomDataGenerator.Tests
{
    public class TestRecordGeneratorMap : GeneratorMap
    {
        public TestRecordGeneratorMap()
        {
            var properties = typeof(TestRecord).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            Map.TryAdd(properties.FirstOrDefault(pi => pi.Name == nameof(TestRecord.UID)),
                (GeneratableTypes.Guid, 0x0, null, 0x0, new object[0x0]));
            Map.TryAdd(properties.FirstOrDefault(pi => pi.Name == nameof(TestRecord.Name)),
                (GeneratableTypes.Name, 0x0, null, 0x0, new object[0x0]));
            Map.TryAdd(properties.FirstOrDefault(pi => pi.Name == nameof(TestRecord.DateOfBirth)),
                (GeneratableTypes.Date, 0x0, null, 0x0,
                    new object[] {new DateTime(0x7B2, 0x1, 0x1), new DateTime(0x7C5, 0xC, 0x1F)}));
            Map.TryAdd(properties.FirstOrDefault(pi => pi.Name == nameof(TestRecord.EmailAddress)),
                (GeneratableTypes.Email, 0x0, null, 0x0, new object[0x0]));
            Map.TryAdd(properties.FirstOrDefault(pi => pi.Name == nameof(TestRecord.ChildRecords)),
                (GeneratableTypes.SimpleChildRecord, 0x0,
                    typeof(SimpleRecordGenerator<TestChildRecord, TestChildRecordGeneratorMap>), 0xA, new object[0x0]));
        }
    }

    public class TestChildRecordGeneratorMap : GeneratorMap
    {
        public TestChildRecordGeneratorMap()
        {
            var properties = typeof(TestChildRecord).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            Map.TryAdd(properties.FirstOrDefault(pi => pi.Name == nameof(TestChildRecord.UID)),
                (GeneratableTypes.Guid, 0x0, null, 0x0, new object[0x0]));
            Map.TryAdd(properties.FirstOrDefault(pi => pi.Name == nameof(TestChildRecord.PlacedOn)),
                (GeneratableTypes.Date, 0x0, null, 0x0, new object[0x0]));
            Map.TryAdd(properties.FirstOrDefault(pi => pi.Name == nameof(TestChildRecord.Number)),
                (GeneratableTypes.Sequence, 0x0, null, 0x0, new object[0x0]));
        }
    }
}