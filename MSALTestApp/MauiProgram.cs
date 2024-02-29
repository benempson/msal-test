using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MSALTestApp.Platforms.PartialClasses;
using MSALTestApp.Services;
using System.Reflection;

namespace MSALTestApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            ServiceProvider sp = builder.Services.BuildServiceProvider();
            Assembly a = Assembly.GetExecutingAssembly();
            using (Stream s = a.GetManifestResourceStream("MSALTestApp.appSettings.json")!)
            {
                IConfigurationRoot config = new ConfigurationBuilder()
                    .AddJsonStream(s)
                    .Build();

                builder.Configuration.AddConfiguration(config);
                Settings settings = config.GetRequiredSection("Settings").Get<Settings>()!;
                SecureStorage.SetAsync("ClientId", settings.ClientId!);
            }

            //do platform specific setup (eg. Android, Windows etc)
            builder.Services.AddSingleton<IServiceCollection>(builder.Services);
            builder.Services.AddScoped<IPlatformSetup, PlatformSetup>();
            sp = builder.Services.BuildServiceProvider();
            IPlatformSetup platformSetup = sp.GetRequiredService<IPlatformSetup>();
            platformSetup.Setup();
            sp = builder.Services.BuildServiceProvider();

            return builder.Build();
        }
    }
}
