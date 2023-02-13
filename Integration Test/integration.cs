using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;
using FarmAdvisor.HttpFunctions;
using FarmAdvisor_HttpFunctions.Functions;
using FarmAdvisor.DataAccess.MSSQL.Functions.Interfaces;
using Microsoft.Extensions.Logging;
using Integration_Test;
using System.Security.Claims;
using FarmAdvisor.Models.Models;

namespace FarmAdvisor.IntegrationTest
{
    public sealed class TestUserFunctions
    {
        readonly UserApi _userFunctions;
        public TestUserFunctions()
        {
            var startup = new HttpFunctionStartup();
            var host = new HostBuilder()
                .ConfigureWebJobs(startup.Configure)
                .Build();

            _userFunctions = new UserApi(host.Services.GetRequiredService<ICrud>());
        }

        

        [Fact]
        public async Task AddUserTest()
        {
           
            var httpContext = new DefaultHttpContext();
            
            var request = httpContext.Request;
            var jsonResult = await _userFunctions.AddUser(request);
            var result = (OkObjectResult)jsonResult.Result;
            var final = (UserModel)result.Value;


            Assert.Equal(final.Name, "user");
            
        }
    }
}