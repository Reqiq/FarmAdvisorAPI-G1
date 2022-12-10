using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FarmAdvisor.Business;
using System;
using System.Net.Http;
using FarmAdvisor.DataAccess.MSSQL.Functions.Crud;
using FarmAdvisor.Business.Imitations;

[assembly: FunctionsStartup(typeof(FarmAdvisor.HttpFunctions.HttpFunctionStartup))]

namespace FarmAdvisor.HttpFunctions
{
    public class HttpFunctionStartup : FunctionsStartup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<CRUD>();
            services.AddTransient<SensorsLogic>();
            services.AddTransient<ScheduledTask>();
            services.AddTransient<WeatherStorageService>();
            services.AddHttpClient<WeatherForecastApi>();
        }


        public override void Configure(IFunctionsHostBuilder builder)
            => ConfigureServices(builder.Services);

    }
}