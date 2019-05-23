namespace AzureFunctionWithDependencyInjectionAndConfigInKeyvault
{
	public class MyConfiguration
	{
		public string ConfigurationItem1 { get; set; }
		public string ConfigurationItem2 { get; set; }
		public string ConfigurationItem3 { get; set; }
		public string ConfigurationItem4 { get; set; }
		public SubConfiguration SubConfiguration { get; set; }
	}

	public class SubConfiguration
	{
		public string SubConfigurationItem { get; set; }
	}
}