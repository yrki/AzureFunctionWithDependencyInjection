using Microsoft.Extensions.Options;

namespace AzureFunctionWithDependencyInjectionAndConfigInKeyvault
{
	public class MyService
	{
		private readonly MyConfiguration _config;

		public MyService(IOptions<MyConfiguration> options)
		{
			// Setting the configuration from options
			_config = options?.Value;
		}

		public string GetValuesFromConfiguration()
		{
			if (_config == null)
			{
				return "Configuration is empty.";
			}

			// Returning all configuration as a string.
			return $"ConfigurationItem1: {_config.ConfigurationItem1}, ConfigurationItem2: {_config.ConfigurationItem2}, ConfigurationItem3: {_config.ConfigurationItem3}, ConfigurationItem4: {_config.ConfigurationItem4}, SubConfigurationItem: {_config.SubConfiguration?.SubConfigurationItem}";
		}


	}
}