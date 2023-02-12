using FarmAdvisor.DataAccess.AzureTableStorage.services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(FarmAdvisor.HttpFunctions.HttpFunctionStartup))]

namespace FarmAdvisor.HttpFunctions
{
    public class HttpFunctionStartup : FunctionsStartup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ICrud, Crud>();
            services.AddScoped<ITableStorageService, TableStorageService>();
            services.AddTransient<ICrud, Crud>();
        }


        public override void Configure(IFunctionsHostBuilder builder)
            => ConfigureServices(builder.Services);

    }
}