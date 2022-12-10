using System;
using System.Threading.Tasks;
using FarmAdvisor.Business;
using Microsoft.Azure.WebJobs;
//using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FarmAdvisor_HttpFunctions.Functions
{
    public class SiteForeCast
    {
        public ScheduledTask scheduledTask;

        public SiteForeCast(ScheduledTask scheduledTask)
        {
            this.scheduledTask= scheduledTask;
        }

        [FunctionName("SiteForeCast")]
        public async Task Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            await scheduledTask.Run();

        }
    }
}
