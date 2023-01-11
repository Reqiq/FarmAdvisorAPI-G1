using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FarmAdvisor.DataAccess.MSSQL.Entities;
using FarmAdvisor.DataAccess.MSSQL.Functions.Crud;

namespace FarmAdvisor_HttpFunctions.Functions
{
    public static class UserApi
    {
        [FunctionName("UserApi")]
        public static async Task<ActionResult<User>> AddUser(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");


            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            string name = data?.name;
            string phone = data?.phone;
            string email = data?.email;
            string authId = data?.authId;

            var user = new User { Name = name, Phone = phone, Email = email, AuthId = authId };

            var crud = new Crud();
            User responseMessage;
            try
            {
                responseMessage = await crud.Create<User>(user);
            } catch (Exception ex)
            {
                return new NotFoundObjectResult(ex);
            }
            return new OkObjectResult(responseMessage);
        }


        [FunctionName("UserApiNew")]
        public static async Task<IActionResult> GetUserNew(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "UserApi/{id}")] HttpRequest req, Guid id,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var crud = new Crud();
            User responseMessage;
            
            try
            {
                responseMessage = await crud.Find<User>(id);
            } catch (Exception ex)
            {
                return new NotFoundObjectResult(ex);
            }
            return new OkObjectResult(responseMessage);
        }
        [FunctionName("UserApiEdit")]
        public static async Task<ActionResult<User>> EditUser(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "UserApi/{id}")] HttpRequest req,Guid id,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");


            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            var crud = new Crud();
            var user = await crud.Find<User>(id);

            user.Name = data?.name;
            user.Phone = data?.phone;
            user.Email = data?.email;
            user.AuthId = data?.authId;


            User responseMessage;
            try
            {
                responseMessage = await crud.Update<User>(id, user);
            }
            catch (Exception ex)
            {
                return new NotFoundObjectResult(ex);
            }
            return new OkObjectResult(responseMessage);
        }

    }

}
