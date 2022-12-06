using FarmAdvisor.Services.WeatherApi.SerializationModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
namespace FarmAdvisor.Services.WeatherApi
{
    
    public class WeatherForecastApi
    {
        private readonly HttpClient _httpCLient = new HttpClient();
    
        public async Task<Weather?> GetLocationForecast(double latitude, double longitude, double altitude)
        {
            _httpCLient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/108.0.0.0 Safari/537.36");
            Weather? response;
            try
            {
            response =  await _httpCLient.GetFromJsonAsync<Weather>($"https://api.met.no/weatherapi/locationforecast/2.0/complete?lat={latitude.ToString()}&lon={longitude.ToString()}&altitude={altitude.ToString()}");
            }
            catch(Exception ex)
            {
                response = null;
            }
            return response;
        }

        public static Dictionary<string, double>[] GetMinAndMaxTempFor8Days(Weather weather)
        {
            Dictionary<string, double>[] forecast = new Dictionary<string, double>[8];

            foreach (var instant in weather.Properties.Timeseries)
            {
                int index = (instant.Time - DateTime.Now).Days;
                if (index > 7) continue;
                double temp_val = instant.Data.Instant.Details["air_temperature"];
                if (!forecast[index].ContainsKey("min") || temp_val < forecast[index]["min"])
                {
                    forecast[index]["min"] = temp_val;
                }
                if (!forecast[index].ContainsKey("max") || temp_val > forecast[index]["max"])
                {
                    forecast[index]["max"] = temp_val;
                }
            }
            return forecast;
        }
    }
}
