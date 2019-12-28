using System;
using System.Collections.Generic;
using System.Reflection;

namespace GPS.RandomDataGenerator.RecordGenerators
{
    public class GeneratorMap
    {
        public readonly Dictionary<PropertyInfo, (GeneratableTypes type, int seed, Type childGeneratorType, int childCount,
            object[] options)> Map =
            new Dictionary<PropertyInfo, (GeneratableTypes type, int seed, Type childGeneratorType, int childCount,
                object[] options)>();
    }
}