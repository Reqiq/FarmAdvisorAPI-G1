using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Data.Tables;
using FarmAdvisor.DataAccess.AzureTableStorage.Models;
using Microsoft.Extensions.Configuration;

namespace FarmAdvisor.DataAccess.AzureTableStorage.services
{
    public class SensorDataStorageService : ISensorDataStorageService
    {

        private const string TableName = "SensorData";
        private readonly IConfiguration _configuration;
        private readonly string connectionString;

        public SensorDataStorageService(IConfiguration configuration)
        {
            _configuration = configuration;
            this.connectionString = "DefaultEndpointsProtocol=https;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;TableEndpoint=https://127.0.0.1:10002/devstoreaccount1;";
        }

        private async Task<TableClient> GetTableClient()
        {
            var serviceClient = new TableServiceClient(connectionString);

            var tableClient = serviceClient.GetTableClient(TableName);
            await tableClient.CreateIfNotExistsAsync();
            return tableClient;
        }

        public async Task<SensorData> GetEntityAsync(string paritionKey, string rowKey)
        {
            var tableClient = await GetTableClient();
            return await tableClient.GetEntityAsync<SensorData>(paritionKey, rowKey);
        }

        public async Task<SensorData> UpsertEntityAsync(SensorData entity)
        {
            var tableClient = await GetTableClient();
            await tableClient.UpsertEntityAsync(entity);
            return entity;
        }

        public async Task DeleteEntityAsync(string paritionKey, string rowKey)
        {
            var tableClient = await GetTableClient();
            await tableClient.DeleteEntityAsync(paritionKey, rowKey);
        }

    }
}