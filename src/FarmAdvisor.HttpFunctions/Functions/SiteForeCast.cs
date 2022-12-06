using System;
using System.Threading.Tasks;
using FarmAdvisor.Services.WeatherApi;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FarmAdvisor_HttpFunctions.Functions
{
    public class SiteForeCast
    {
        public WeatherForecastApi _weatherForecastApi;

        public SiteForeCast(WeatherForecastApi weatherForecastApi)
        {
            _weatherForecastApi= weatherForecastApi;
        }

        [FunctionName("SiteForeCast")]
        public async Task Run([TimerTrigger("0 0 10 * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            var result = await _weatherForecastApi.GetLocationForecast(-16.516667, -68.166667, 4150);
            if (result != null)
                log.LogInformation($"{WeatherForecastApi.GetMinAndMaxTempFor8Days(result)}");
            log.LogInformation($"{result}");
        }
    }
}
