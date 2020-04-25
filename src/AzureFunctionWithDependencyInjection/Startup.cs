using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// Needs the: Microsoft.Azure.Functions.Extensions - nuget package
[assembly: FunctionsStartup(typeof(AzureFunctionWithDependencyInjectionAndConfigInKeyvault.Startup))]

namespace AzureFunctionWithDependencyInjectionAndConfigInKeyvault
{
	// : FunctionsStartup - needs the: Microsoft.Azure.Functions.Extensions - nuget package
	public class Startup : FunctionsStartup
	{
		public override void Configure(IFunctionsHostBuilder builder)
		{
			var environment = Environment.GetEnvironmentVariable("Environment");

			// The order of the .AddKeyVault/.AddJsonFile/.AddEnvironmentVariables matters. Next overrides previous etc.

			var config = new ConfigurationBuilder()
				// setting the base path
				.SetBasePath(Environment.CurrentDirectory)

				// Setting the key vault (See the Configuration builder extensions)
				// Key format in AzureKeyVault: "MyConfiguration__ConfigurationItem1"
				//.AddKeyVault(environment)

				// Setting the default settings from json file
				.AddJsonFile($"settings.json", optional: false)

				// Setting the environment specific setting from json file.
				.AddJsonFile($"{environment}.settings.json", optional: true)

				// Setting settings from environment-variables
				// Key format in environment variables: "MyConfiguration--ConfigurationItem1"
				.AddEnvironmentVariables()
				.Build();

			// Adding service registry
			builder.Services.AddSingleton<IMyService, MyService>();

			// Setting MyConfiguration
			builder.Services.Configure<MyConfiguration>(config.GetSection("MyConfiguration"));
		}
	}
}