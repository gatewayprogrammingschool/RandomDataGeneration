using System;
using System.Collections.Generic;

namespace GPS.RandomDataGenerator.Abstractions
{
    public interface IGeneratorOptions<TResult>
    {
        IEnumerable<TResult> Generate(Random random);
    }
}