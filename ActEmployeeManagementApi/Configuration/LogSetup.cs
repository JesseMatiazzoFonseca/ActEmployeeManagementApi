using Domain.Settings;
using Microsoft.Extensions.Configuration;


namespace API.Configuration
{
    public static class LogSetup
    {
        public static IServiceCollection AddSetupLog(this IServiceCollection services, AppSettingsConfig appSettings)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
                builder.AddFile(appSettings.GetPathLogError(), Microsoft.Extensions.Logging.LogLevel.Error);
                builder.AddFile(appSettings.GetPathLogWarning(), Microsoft.Extensions.Logging.LogLevel.Warning);
                builder.AddFile(appSettings.GetPathLogInformation(), Microsoft.Extensions.Logging.LogLevel.Information);
            });
            return services;
        }
    }
}
