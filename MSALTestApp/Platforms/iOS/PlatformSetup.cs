using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using MSALTestApp.Services;
using System.Reflection.Metadata;

namespace MSALTestApp.Platforms.PartialClasses;

public partial class PlatformSetup : IPlatformSetup
{
    private ILogger<PlatformSetup> _logger;
    private IServiceCollection _serviceCollection;

    #region Constructor
    public PlatformSetup(ILogger<PlatformSetup> logger, IServiceCollection srvCollection)
    {
        _logger = logger;
        _serviceCollection = srvCollection;
    }
    #endregion

    #region Setup
    public void Setup()
    {
        _logger.LogInformation("Enter iOS Setup");

        string? clientId = SecureStorage.GetAsync("ClientId").Result;

        IPublicClientApplication pca = PublicClientApplicationBuilder
            .Create(clientId)
            .WithAuthority(AzureCloudInstance.AzurePublic, "common")
            .WithIosKeychainSecurityGroup("com.microsoft.adalcache")
            .WithRedirectUri($"msal{clientId}://auth")
            .Build();

        _serviceCollection.AddSingleton<IPublicClientApplication>(pca);
    }
    #endregion
}
