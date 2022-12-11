using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Busineess.Models;
using FarmAdvisor.DataAccess.AzureTableStorage.services;
using Moq;
using Xunit;

namespace FarmAdvisor.Function.Test.DataAccess.AzureTableStorage
{
    public class WeatherStorageServiceTests
    {
        [Fact]
        public async Task TestConnection()
        {

            var storage = new WeatherStorageService();
            try {
                await storage.DeleteEntityAsync("ad", "ad");
                Assert.True(false);
            }
            catch {
                Assert.True(true);
            }
            // Assert.Throws<System.Exception>();
            //await storage.UpsertEntityAsync(weather);


        //     var mock = new Mock<IWeatherStorageService>();

        //     mock.Setup(weatherStorageService => weatherStorageService.UpsertEntityAsync(It.IsAny<Weather>())).Callback(() =>
        // {
        //    mock.Setup(weatherStorageService => weatherStorageService.GetEntityAsync(It.IsAny<string>(), It.IsAny<string>()))
        //         .Returns(async () => await Task.FromResult(Content));
        //     mock.Setup(azureBlobStorage => azureBlobStorage.NumberOfBlobs())
        //         .Returns(1);
        // });
        }


        [Fact]
        public async Task UpsertAndGetEntityTest() {
            var storage = new WeatherStorageService();
            var weather = new Weather();
            weather.AirPressureAtSeaLevel = 20;
            weather.CloudAreaFraction = 20;
            weather.RelativeHumidity = 10;
            weather.WindSpeed = 15;
            weather.WindFromDirection = 19;
            weather.PartitionKey = "10";
            weather.RowKey = "01";

            var result = await storage.UpsertEntityAsync(weather);

            var added = await storage.GetEntityAsync(weather.PartitionKey, weather.RowKey);

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
            var storage = new WeatherStorageService();
            var weather = new Weather();
            weather.AirPressureAtSeaLevel = 20;
            weather.CloudAreaFraction = 20;
            weather.RelativeHumidity = 10;
            weather.WindSpeed = 15;
            weather.WindFromDirection = 19;
            weather.PartitionKey = "10";
            weather.RowKey = "01";

            await storage.UpsertEntityAsync(weather);
            await storage.DeleteEntityAsync(weather.PartitionKey, weather.RowKey);

            await Assert.ThrowsAsync<Azure.RequestFailedException>(async ()=> await storage.GetEntityAsync(weather.PartitionKey, weather.RowKey));

            // Assert.Null(removed);
        }

    }
}