using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Azure.Data.Tables;
namespace FarmAdvisor.Models.Models
{
    public class WeatherModel: ITableEntity
    {
        public double AirPressureAtSeaLevel { get; set; }
        public double CloudAreaFraction { get; set; }
        public double RelativeHumidity { get; set; }
        public double WindFromDirection { get; set; }
        public double WindSpeed { get; set; }
        public string? PartitionKey { get; set; }
        public string? RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
