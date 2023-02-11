using FarmAdvisor.HttpFunctions;
using FarmAdvisor.HttpFunctions.Functions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using FarmAdvisor.DataAccess.MSSQL.DataContext;
using Microsoft.EntityFrameworkCore;

namespace FarmAdvisor.Functions.Test.HttpFunctionsTest.Infrastructure
{
    public class HttpFunctionsTestStartup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<FarmFieldFunctions>();
            services.AddDbContext<DatabaseContext>(options => { options.UseInMemoryDatabase("TestDb"); });
            HttpFunctionStartup.ConfigureServices(services);

        }
    }
}
