using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using MSALTestApp.Services;

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
        _logger.LogInformation("Enter Android Setup");

        string? clientId = SecureStorage.GetAsync("ClientId").Result;

        IPublicClientApplication pca = PublicClientApplicationBuilder
            .Create(clientId)
            .WithAuthority(AzureCloudInstance.AzurePublic, "common")
            .WithRedirectUri($"msal{clientId}://auth")
            .WithParentActivityOrWindow(() => Platform.CurrentActivity)
            .Build();

        _serviceCollection.AddSingleton<IPublicClientApplication>(pca);
        _logger.LogInformation("Exit Android Setup");
    }
    #endregion
}
