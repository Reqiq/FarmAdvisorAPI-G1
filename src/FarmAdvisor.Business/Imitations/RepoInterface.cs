namespace FarmAdvisor.Business.Imitations
{
    public interface IweatherStorageService
    {
        public Task<Weather?> GetEntityAsync(string SensorId, string Timestamp);

        public Task<Weather?> UpsertEntityAsync(Weather entity);

        public Task? DeleteEnityAsync(string SensorId, string Timestamp);

        public Task<List<Weather>?> GetWeatherStartingFromDate(String sensorId,DateTimeOffset TimeStamp);
    }

    public class WeatherStorageService: IweatherStorageService
    {
        public Task<Weather?> GetEntityAsync(string SensorId, string Timestamp)
        {
            return null;
        }

        public Task<Weather?> UpsertEntityAsync(Weather entity)
        {
            return null;
        }

        public Task? DeleteEnityAsync(string SensorId, string Timestamp)
        {
            return null;
        }

        public Task<List<Weather>?> GetWeatherStartingFromDate(String sensorId, DateTimeOffset TimeStamp)
        {
            return null;
        }
    }
}
