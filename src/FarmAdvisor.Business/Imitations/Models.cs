using Azure;
using Azure.Data.Tables;

namespace FarmAdvisor.Business.Imitations
{
   
        public class Weather : ITableEntity
        {

            public string PartitionKey { get; set; }
            public string RowKey { get; set; }
            public DateTimeOffset? Timestamp { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public ETag ETag { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            public double AirPressureAtSeaLevel { get; set; }
            public double CloudAreaFraction { get; set; }
            public double RelativeHumidity { get; set; }
            public double WindFromDirection { get; set; }
            public double WindSpeed { get; set; }
            public double Temperature { get; set; } 

        }
        public class CalculatedGDD : ITableEntity
        {
            public DateOnly ComputedTime { get; set; }
            public int SensorId { get; set; }
            public double Value { get; set; } = 0;
            public string PartitionKey { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public string RowKey { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public DateTimeOffset? Timestamp { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public ETag ETag { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        }

}
