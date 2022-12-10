using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Busineess.Models;

namespace FarmAdvisor.DataAccess.AzureTableStorage.services
{
    public interface IWeatherStorageService
    {

    Task<Weather> GetEntityAsync(string SensorId, string Timestamp);
    Task<Weather> UpsertEntityAsync(Weather entity);
    Task DeleteEntityAsync(string SensorId, string Timestamp);
    Task<List<Weather>> GetWeatherStartingFromDate(DateTimeOffset TimeStamp);

    }
}