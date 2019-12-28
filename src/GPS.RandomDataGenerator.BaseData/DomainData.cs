using GPS.RandomDataGenerator.Abstractions;
// ReSharper disable UnusedType.Global

namespace GPS.RandomDataGenerator.BaseData
{
    public class DomainData : IDataProvider<string>
    {
        public string[] Data => new[]
                                {
                                    "mailinator.com",
                                    "jetable.com"
                                };
    }
}