using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Azure.Data.Tables;
using Busineess.Models;
using FarmAdvisor.DataAccess.AzureTableStorage.Models;
using Microsoft.Extensions.Configuration;

namespace FarmAdvisor.DataAccess.AzureTableStorage.services
{
    public class WeatherStorageService : IWeatherStorageService
    {

        private const string TableName = "Weather";
        private readonly string connectionString;
        // private readonly IConfiguration configuration;

        public WeatherStorageService()
        {
            // this.configuration = configuration;
            this.connectionString = "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;";
            
        }

        private async Task<TableClient> GetTableClient() {
            var serviceClient = new TableServiceClient(connectionString);

            var tableClient = serviceClient.GetTableClient(TableName);
            await tableClient.CreateIfNotExistsAsync();

            return tableClient;
        }

        public async Task DeleteEntityAsync(string partitionKey, string rowKey)
        {
            var tableClient = await GetTableClient();

            await tableClient.DeleteEntityAsync(partitionKey, rowKey);
        }

        public async Task<Weather> GetEntityAsync(string partitionKey, string rowKey)
        {
            var tableClient = await GetTableClient();
            
            return await tableClient.GetEntityAsync<Weather>(partitionKey, rowKey);
        }

        public async Task<Weather> UpsertEntityAsync(Weather entity)
        {
            var tableClient = await GetTableClient();

            await tableClient.UpsertEntityAsync<Weather>(entity);
            return entity;
        }

        public async Task<List<Weather>> GetWeatherStartingFromDate(DateTimeOffset TimeStamp) {
            var tableClient = await GetTableClient();

            Pageable<Weather> queryResultsFilter = tableClient.Query<Weather>(filter: $"TimeStamp gt '{TimeStamp}'");

            return queryResultsFilter.ToList<Weather>();
        }
    }
}