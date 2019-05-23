using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureFunctionWithDependencyInjectionAndConfigInKeyvault
{
    public class MyFunctionLibrary
    {
        [FunctionName("MyFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log,
            ExecutionContext context)
        {
			// Dependency injection - setup
            var serviceProvider = BootStrap.Startup(context);

			// Get the service
            var myLogic = serviceProvider.GetService<MyService>();

			// Use the service to get values from configuration
            var result = myLogic.GetValuesFromConfiguration();

            return (ActionResult) new OkObjectResult(result);
        }
    }
}
