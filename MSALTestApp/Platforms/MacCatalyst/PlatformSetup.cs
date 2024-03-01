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
    public async Task Setup()
    {
        _logger.LogInformation("Enter MacCatalyst Setup");

        string? clientId = await SecureStorage.GetAsync("ClientId");
        PublicClientApplicationBuilder pcab = PublicClientApplicationBuilder
            .Create(clientId)
            .WithAuthority(AzureCloudInstance.AzurePublic, "common")
            .WithRedirectUri("https://login.microsoftonline.com/common/oauth2/nativeclient");

        _serviceCollection.AddSingleton<IPublicClientApplication>(pcab.Build());
    }
    #endregion
}
