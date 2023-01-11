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
using FarmAdvisor.DataAccess.MSSQL.Functions.Interfaces;
using FarmAdvisor.DataAccess.MSSQL.DataContext;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FarmAdvisor_HttpFunctions.Functions
{
    public class UserApi
    {
        public ICrud _crud;
        public UserApi(ICrud crud)
        {
            _crud= crud;
        }
        [FunctionName("UserApi")]
        public async Task<ActionResult<User>> AddUser(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");


            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            string phone = data?.phone;

            User prevUser;
            using (var context = new DatabaseContext(DatabaseContext.Options.DatabaseOptions))
            {
                prevUser = await context.Users.FirstOrDefaultAsync(s => s.Phone == phone);
            }
            if (prevUser != null)
            {
                return new ConflictObjectResult("Phone exists");
            }

            string name = data?.name;
            string email = data?.email;
            string authId = data?.authId;

            var user = new User { Name = name, Phone = phone, Email = email, AuthId = authId };

            User responseMessage;
            try
            {
                responseMessage = await _crud.Create<User>(user);
            } catch (Exception ex)
            {
                return new NotFoundObjectResult(ex);
            }
            return new OkObjectResult(responseMessage);
        }


        [FunctionName("UserApiNew")]
        public async Task<IActionResult> GetUserNew(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "UserApi/{id}")] HttpRequest req, Guid id,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            User responseMessage;
            
            try
            {
                responseMessage = await _crud.Find<User>(id);
            } catch (Exception ex)
            {
                return new NotFoundObjectResult(ex);
            }
            return new OkObjectResult(responseMessage);
        }
        [FunctionName("UserApiEdit")]
        public async Task<ActionResult<User>> EditUser(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "UserApi/{id}")] HttpRequest req,Guid id,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");


            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            var user = await _crud.Find<User>(id);

            user.Name = data?.name;
            user.Phone = data?.phone;
            user.Email = data?.email;
            user.AuthId = data?.authId;


            User responseMessage;
            try
            {
                responseMessage = await _crud.Update<User>(id, user);
            }
            catch (Exception ex)
            {
                return new NotFoundObjectResult(ex);
            }
            return new OkObjectResult(responseMessage);
        }

    }

}
