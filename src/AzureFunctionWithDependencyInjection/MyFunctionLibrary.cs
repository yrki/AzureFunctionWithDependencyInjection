using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace AzureFunctionWithDependencyInjectionAndConfigInKeyvault
{
    public class MyFunctionLibrary
    {
        private readonly IMyService _service;

        public MyFunctionLibrary(IMyService service)
        {
            _service = service;
        }


        [FunctionName("MyFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log,
            ExecutionContext context)
        {
			// Use the service to get values from configuration
            var result = _service.GetValuesFromConfiguration();

            return (ActionResult) new OkObjectResult(result);
        }
    }
}
