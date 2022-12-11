using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Azure.Data.Tables;

namespace FarmAdvisor.Models.Models
{
        public class CalculatedGdd: ITableEntity
    {
        // public DateOnly ComputedTime{ get; set; }
        public string SensorId { get; set; }
        public double Value { get; set; } = 0;
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}