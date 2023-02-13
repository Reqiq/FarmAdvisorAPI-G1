using FarmAdvisor.DataAccess.MSSQL.DataContext;
using FarmAdvisor.HttpFunctions;
using FarmAdvisor.Models.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Drawing;
using Microsoft.Extensions.Hosting;

namespace Integration_Test
{
    public class App
    {

        public App()
        {

            var startup = new HttpFunctionStartup();
            var host = new HostBuilder()
                .ConfigureWebJobs(startup.Configure)
                .Build();
        }
    }
    public class IntegrationTest

    {
        protected readonly HttpClient httpClient;
        public IntegrationTest() {
            var appFactory = new WebApplicationFactory<>(new App())
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(DatabaseContext));
                        services.AddDbContext<DatabaseContext>(options =>
                        {
                            options.UseInMemoryDatabase("TestDb");
                        });
                    }); 
                });
            httpClient = appFactory.CreateClient();

        }
    protected async Task<UserModel> CreateUserAsync()
        {
            var response = await httpClient.PostAsJsonAsync("http://localhost:7071/api/AddUserApi", new UserModel
            {
                UserID = new Guid(),
                Name = "yoseph",
                Email = "integration@test.com",
                AuthId = "32456tyredweqrtgtre23324345645r",
                Phone = "+251943556456"
            }) ;
            ;
            var mockUser = await response.Content.ReadAsAsync<UserModel>();
            return mockUser;

        }

    }
}