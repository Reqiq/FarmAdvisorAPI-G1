using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Azure.Data.Tables;

namespace FarmAdvisor.DataAccess.AzureTableStorage.Models
{
    public class SensorData : ITableEntity
    {
        
        public int SerialNum { get; set; }
        [Required]
        public bool BatteryStatus { get; set; }
        [Required]
        public int MeasurementPeriodBase { get; set; }
        [Required]
        public Dictionary<string, dynamic>? Channels { get; set; }
        [Required]
        public int NextTransmissionAt { get; set; }
        [Required]
        public int Signal { get; set; }
        [Required]
        public int CloudToken { get; set; }
        public string PartitionKey { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string RowKey { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTimeOffset? Timestamp { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ETag ETag { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}