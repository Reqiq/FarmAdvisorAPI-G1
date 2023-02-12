using FarmAdvisor.DataAccess.MSSQL.DataContext;
using FarmAdvisor.Functions.Test.HttpFunctionsTest.Infrastructure;
using FarmAdvisor.HttpFunctions;
using FarmAdvisor.Models.Models;
using FarmAdvisor_HttpFunctions.Functions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FarmAdvisor.Function.Test.HttpFunctionTest
{
    public class IntegrationTest
    {
        protected readonly HttpClient _TestClient;

        public IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<HttpFunctionsTestStartup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(configureServices =>
                    {
                        configureServices.RemoveAll(typeof(DatabaseContext));
                        configureServices.AddDbContext<DatabaseContext>(options => { options.UseInMemoryDatabase("TestDb"); });
                    });
                });

            _TestClient = appFactory.CreateClient();
        }

        protected async Task<HttpResponseMessage> PostUserModel()
        {
            var response = await _TestClient.PostAsJsonAsync("/AddUserApi", new UserModel{ AuthId = "123423424", Name= "semir", Phone= "0912345678", Email= "Semir2578@gmail.com"});
            return response;
        }

        [Fact]

        public async Task ReturnOkWhenPost()
        {
            var response = await PostUserModel();

            Assert.Equal(response.IsSuccessStatusCode, true);
        }
    }
}
