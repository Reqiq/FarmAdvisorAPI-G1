using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Busineess.Models;
using FarmAdvisor.DataAccess.AzureTableStorage.services;
using Xunit;

namespace FarmAdvisor.Function.Test.DataAccess.AzureTableStorage
{
    public class TableStorageServiceTests
    {

        [Fact]
        public async Task TestConnection()
        {
            string tableName = "Weather";
            var storage = new TableStorageService(tableName);
            try {
                await storage.DeleteEntityAsync<Weather>("ad", "ad");
                Assert.True(false);
            }
            catch {
                Assert.True(true);
            }
        }


        [Fact]
        public async Task UpsertAndGetEntityTest() {
            string tableName = "Weather";
            var storage = new TableStorageService(tableName);
            var weather = new Weather();
            weather.AirPressureAtSeaLevel = 20;
            weather.CloudAreaFraction = 20;
            weather.RelativeHumidity = 10;
            weather.WindSpeed = 15;
            weather.WindFromDirection = 19;
            weather.PartitionKey = "10";
            weather.RowKey = "01";

            var result = await storage.UpsertEntityAsync<Weather>(weather);

            var added = await storage.GetEntityAsync<Weather>(weather.PartitionKey, weather.RowKey);

            Assert.Multiple(
                () => Assert.Equal(result.AirPressureAtSeaLevel, added.AirPressureAtSeaLevel),
                () => Assert.Equal(result.CloudAreaFraction, added.CloudAreaFraction),
                () => Assert.Equal(result.RelativeHumidity, added.RelativeHumidity),
                () => Assert.Equal(result.WindSpeed, added.WindSpeed),
                () => Assert.Equal(result.WindFromDirection, added.WindFromDirection),
                () => Assert.Equal(result.PartitionKey, added.PartitionKey),
                () => Assert.Equal(result.RowKey, added.RowKey)
            );
        }

        [Fact]
        public async Task DeleteEntityTest() {
            string tableName = "Weather";
            var storage = new TableStorageService(tableName);
            var weather = new Weather();
            weather.AirPressureAtSeaLevel = 20;
            weather.CloudAreaFraction = 20;
            weather.RelativeHumidity = 10;
            weather.WindSpeed = 15;
            weather.WindFromDirection = 19;
            weather.PartitionKey = "10";
            weather.RowKey = "01";

            await storage.UpsertEntityAsync<Weather>(weather);
            await storage.DeleteEntityAsync<Weather>(weather.PartitionKey, weather.RowKey);

            await Assert.ThrowsAsync<Azure.RequestFailedException>(async ()=> await storage.GetEntityAsync<Weather>(weather.PartitionKey, weather.RowKey));

            // Assert.Null(removed);
        }
    }
}