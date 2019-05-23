using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;

namespace AzureFunctionWithDependencyInjectionAndConfigInKeyvault
{
	public static class ConfigurationBuilderExtensions
	{
		public static IConfigurationBuilder AddKeyVault(this IConfigurationBuilder builder, string environment)
		{
			var azureServiceTokenProvider = new AzureServiceTokenProvider();
			var keyVaultClient = new KeyVaultClient(
				new KeyVaultClient.AuthenticationCallback(
					azureServiceTokenProvider.KeyVaultTokenCallback));

			// Add azure keyvault
			builder.AddAzureKeyVault($"https://MyKeyVaultName{environment}.vault.azure.net/", 
				keyVaultClient,
				new DefaultKeyVaultSecretManager());

			return builder;
		}
	}
}