using System.Collections.Generic;
// ReSharper disable UnusedMemberInSuper.Global

namespace GPS.RandomDataGenerator.Abstractions
{
    public interface IDataGenerator
    {
    }

    public interface IResetable
    {
        void Reset(int seed);
    }

    public interface IDataGenerator<out TData> : IDataGenerator
    {
        IEnumerable<TData> Generate(int? seed, int count, params object[] options);
    }
}