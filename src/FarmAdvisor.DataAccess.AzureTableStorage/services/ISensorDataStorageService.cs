using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmAdvisor.DataAccess.AzureTableStorage.Models;

namespace FarmAdvisor.DataAccess.AzureTableStorage.services
{
    public interface ISensorDataStorageService
    {
        Task<SensorData> GetEntityAsync(string category, string id);
        Task<SensorData> UpsertEntityAsync(SensorData entity);
        Task DeleteEntityAsync(string category, string id);
    }
}