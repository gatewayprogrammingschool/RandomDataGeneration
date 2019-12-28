namespace GPS.RandomDataGenerator.Abstractions
{
    public interface IDataProvider<out TData>
    {
        TData[] Data { get; }
    }
}