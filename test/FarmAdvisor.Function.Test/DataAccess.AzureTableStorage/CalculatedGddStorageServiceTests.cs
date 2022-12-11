using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmAdvisor.DataAccess.AzureTableStorage.services;
using FarmAdvisor.Models.Models;
using Xunit;

namespace FarmAdvisor.Function.Test.DataAccess.AzureTableStorage
{
    public class CalculatedGddStorageServiceTests
    {
        [Fact]
        public async Task TestConnection()
        {
            IGddStorageService storage = new GddStorageService();
            try {
                await storage.DeleteEntityAsync("ad", "ad");
                Assert.True(false);
            }
            catch {
                Assert.True(true);
            }
        }

        [Fact]
        public async Task UpsertAndGetEntityTest() {
            var storage = new GddStorageService();
            var gdd = new CalculatedGdd();
            // gdd.ComputedTime = new DateOnly(2010, 10, 1);
            gdd.SensorId = "001";
            gdd.Value = 10;
            gdd.PartitionKey = "10";
            gdd.RowKey = "01";

            var result = await storage.UpsertEntityAsync(gdd);

            var added = await storage.GetEntityAsync(gdd.PartitionKey, gdd.RowKey);

            Assert.Multiple(
                // () => Assert.Equal(result.ComputedTime, added.ComputedTime),
                () => Assert.Equal(result.SensorId, added.SensorId),
                () => Assert.Equal(result.Value, added.Value),
                () => Assert.Equal(result.PartitionKey, added.PartitionKey),
                () => Assert.Equal(result.RowKey, added.RowKey)
            );
        }

        [Fact]
        public async Task DeleteEntityTest() {
            var storage = new GddStorageService();
            var gdd = new CalculatedGdd();
            // gdd.ComputedTime = new DateOnly(2010, 10, 1);
            gdd.SensorId = "001";
            gdd.Value = 10;
            gdd.PartitionKey = "10";
            gdd.RowKey = "01";

            await storage.UpsertEntityAsync(gdd);
            await storage.DeleteEntityAsync(gdd.PartitionKey, gdd.RowKey);

            await Assert.ThrowsAsync<Azure.RequestFailedException>(async () => await storage.GetEntityAsync(gdd.PartitionKey, gdd.RowKey));

        }

    }
}