using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FarmAdvisor.DataAccess.MSSQL.DataContext;
using FarmAdvisor.DataAccess.MSSQL.Functions.Interfaces;
using FarmAdvisor.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace FarmAdvisor_HttpFunctions.Functionsw
{
    public class FarmEndpoint
    {
        public ICrud _crud;

        public FarmEndpoint(ICrud crud)
        {
            _crud = crud;
        }

        [FunctionName("AddFarmEndpoint")]
        public async Task<ActionResult<FarmModel>> AddFarmModel(
           [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
           ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");


            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            string? userId = req.Headers["UserId"];
            
            if (userId is null)
            {
                return new NotFoundObjectResult("No user Id provided");
            }


            dynamic data = JsonConvert.DeserializeObject(requestBody);

            string Name = data?.name;
            string PostCode = data?.postcode;
            string City = data?.city;
            string Country = data?.country;

            var farm = new FarmModel { Name = Name, Postcode = PostCode, City = City, Country = Country };

            FarmModel responseMessage;
            try
            {
                responseMessage = await _crud.Create<FarmModel>(farm);
            }
            catch (Exception ex)
            {
                return new NotFoundObjectResult(ex);
            }
            return new OkObjectResult(responseMessage);
        }

        [FunctionName("GetFarmEndpoint")]
        public async Task<IActionResult> GetFarmModel(
           [HttpTrigger(AuthorizationLevel.Function, "get", Route = "FarmApi/{id}")] HttpRequest req, Guid id,
           ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            FarmModel responseMessage;

            try
            {
                responseMessage = await _crud.Find<FarmModel>(id);
            }
            catch (Exception ex)
            {
                return new NotFoundObjectResult(ex);
            }
            return new OkObjectResult(responseMessage);
        }
    }
}
