using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Data.Tables;
using FarmAdvisor.Models.Models;

namespace FarmAdvisor.DataAccess.AzureTableStorage.services
{
    public class GddStorageService : IGddStorageService
    {
        private const string TableName = "CalculatedGdd";
        private readonly string connectionString;
        // private readonly IConfiguration configuration;

        public GddStorageService()
        {
            // this.configuration = configuration;
            this.connectionString = "DefaultEndpointsProtocol=https;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;TableEndpoint=https://127.0.0.1:10002/devstoreaccount1;";
            
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

        public async Task<CalculatedGdd> GetEntityAsync(string partitionKey, string rowKey)
        {
            var tableClient = await GetTableClient();
            
            return await tableClient.GetEntityAsync<CalculatedGdd>(partitionKey, rowKey);
        }

        public async Task<CalculatedGdd> UpsertEntityAsync(CalculatedGdd entity)
        {
            var tableClient = await GetTableClient();

            await tableClient.UpsertEntityAsync<CalculatedGdd>(entity);
            return entity;
        }
    }
}