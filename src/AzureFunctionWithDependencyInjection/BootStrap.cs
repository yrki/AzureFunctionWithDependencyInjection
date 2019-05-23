using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AzureFunctionWithDependencyInjectionAndConfigInKeyvault
{
	public class BootStrap
	{
		public static IServiceProvider Startup(ExecutionContext context)
		{
			var environment = Environment.GetEnvironmentVariable("Environment");


			// The order of the .AddKeyVault/.AddJsonFile/.AddEnvironmentVariables matters. Next overrides previous etc.

			var config = new ConfigurationBuilder()
				// setting the base path
				.SetBasePath(Environment.CurrentDirectory)

				// Setting the key vault (See the Configuration builder extensions)
				// Key format in AzureKeyVault: "MyConfiguration__ConfigurationItem1"
				.AddKeyVault(environment)

				// Setting the default settings from json file
				.AddJsonFile($"{context.FunctionAppDirectory}/settings.json", optional: true)

				// Setting the environment specific setting from json file.
				.AddJsonFile($"{context.FunctionAppDirectory}/{environment}.settings.json")

				// Setting settings from environment-variables
				// Key format in environment variables: "MyConfiguration--ConfigurationItem1"
				.AddEnvironmentVariables()
				.Build();

			// Adding service registry
			var services = new ServiceCollection()
				.AddSingleton<MyService>();

			// Setting MyConfiguration
			services.Configure<MyConfiguration>(config.GetSection("MyConfiguration"));
		    
			return services.BuildServiceProvider();
		}
	}
}