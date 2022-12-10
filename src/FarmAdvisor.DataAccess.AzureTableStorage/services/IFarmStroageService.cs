// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using FarmAdvisor.Models.Models;

// namespace FarmAdvisor.DataAccess.AzureTableStorage.services
// {
//     public interface IFarmStroageService
//     {
//         Task<Farm> GetEntityAsync(string SensorId, string Timestamp);
//         Task<CalculatedGdd> UpsertEntityAsync(CalculatedGdd entity);
//         Task DeleteEntityAsync(string SensorId, string Timestamp);
//     }
// }