using FarmAdvisor.Business.Imitations;
using FarmAdvisor.DataAccess.MSSQL.Entities;
using FarmAdvisor.DataAccess.MSSQL.Functions.Crud;

namespace FarmAdvisor.Business
{
    public class SensorsLogic
    {
        public WeatherForecastApi weatherForecastApi{ get; set; }
        public WeatherStorageService weatherStorageServer { get; set; }
        public CRUD _Crud { get; set; }
        private readonly double BaseTemp = 1;
        public SensorsLogic(WeatherForecastApi weatherForecastApi,CRUD Crud, WeatherStorageService weatherStorageService)
        {
            this.weatherForecastApi= weatherForecastApi;
            this.weatherStorageServer = weatherStorageService;
            _Crud= Crud;
        }
        public async Task Run(List<Sensor> sensorList)
        {
            foreach( var sensor in sensorList)
            {
                WeatherForecast? weatherForecast = await weatherForecastApi.GetLocationForecast(sensor);
                if(weatherForecast == null) { continue; }
                SortedDictionary<string, double> data = new SortedDictionary<string, double>();
                SortedDictionary<string, int> quantity = new SortedDictionary<string, int>();
                DateTime lastForecast = DateTime.Now;
                foreach (var inst in weatherForecast.Properties.Timeseries)
                {
                    string date = inst.Time.Date.ToString();
                    if (data.ContainsKey(date))
                    {
                        data[date]+= inst.Data.Instant.Details["air_temperature"];
                        quantity[date] += 1;
                        lastForecast= DateTime.Parse(date);
                    }
                    else
                    {
                        data.Add(date,inst.Data.Instant.Details["air_temperature"]);
                        quantity.Add(date,1);
                    }
                    Weather w = new Weather();
                    w.PartitionKey = sensor.SensorId.ToString();
                    w.RowKey = date;
                    w.Temperature = inst.Data.Instant.Details["air_temperature"];
                    w.AirPressureAtSeaLevel = inst.Data.Instant.Details["air_pressure_at_sea_level"];
                    w.CloudAreaFraction = inst.Data.Instant.Details["cloud_area_fraction"];
                    w.RelativeHumidity = inst.Data.Instant.Details["relative_humidity"];
                    w.WindSpeed = inst.Data.Instant.Details["wind_speed"];
                    w.WindFromDirection = inst.Data.Instant.Details["wind_from_direction"];

                    await weatherStorageServer.UpsertEntityAsync(w);
                }

                double addedGdds = 0;
                //logic for getting current GDD
                foreach(KeyValuePair<string, double> kvp in data.OrderBy(x=> x.Key))
                {
                    double avg_temp = kvp.Value / quantity[kvp.Key];
                    if (avg_temp > BaseTemp) {
                        addedGdds+= (avg_temp- BaseTemp);
                        if (addedGdds >= sensor.OptimalGDD)
                        {
                            sensor.CuttingDateTimeCalculated = Convert.ToDateTime(kvp.Key);
                        }
                        //logic for saving data
                    }

                }
                sensor.LastForecastDate= lastForecast;
                //await _Crud.Update<Sensor>(sensor, sensor.SensorId);
                return;
            }
        }
    }
}
