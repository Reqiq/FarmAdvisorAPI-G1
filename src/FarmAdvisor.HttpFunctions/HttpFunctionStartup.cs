using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FarmAdvisor.Services.WeatherApi;
using System;

[assembly: FunctionsStartup(typeof(FarmAdvisor.HttpFunctions.HttpFunctionStartup))]

namespace FarmAdvisor.HttpFunctions
{
    public class HttpFunctionStartup : FunctionsStartup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            /* services.AddHttpClient<WeatherForecastApi>(client =>
             {
                 client.BaseAddress = new Uri("https://api.met.no/weatherapi/locationforecast/2.0/");
             });*/
            services.AddSingleton<WeatherForecastApi>();
        }


        public override void Configure(IFunctionsHostBuilder builder)
            => ConfigureServices(builder.Services);

    }
}