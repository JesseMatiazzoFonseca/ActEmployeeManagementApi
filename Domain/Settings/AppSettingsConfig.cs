namespace Domain.Settings
{
    public class LogLevel
    {
        public string Information { get; set; }
        public string Warning { get; set; }
        public string Error { get; set; }
    }

    public class Logging
    {
        public LogLevel LogLevel { get; set; }
    }

    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; }
    }

    public class VersionInfo
    {
        public string Version { get; set; }
        public string Description { get; set; }
    }

    public class Application
    {
        public string Name { get; set; }
        public List<VersionInfo> Versions { get; set; }
    }

    public class Security
    {
        public string Key { get; set; }
    }

    public class AppSettings
    {
        public Security Security { get; set; }
        public string CultureInfo { get; set; }
        public string AllowedHosts { get; set; }
    }

    public class AppSettingsConfig
    {
        public Logging Logging { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
        public Application Application { get; set; }
        public AppSettings AppSettings { get; set; }
        public string GetPathLogError()
        {
            return $"{Logging.LogLevel.Error}/{Application.Name}/ERROR-{{Date}}.txt";
        }
        public string GetPathLogInformation()
        {
            return $"{Logging.LogLevel.Information}/{Application.Name}/INFORMATION-{{Date}}.txt";
        }
        public string GetPathLogWarning()
        {
            return $"{Logging.LogLevel.Warning}/{Application.Name}/WARNING-{{Date}}.txt";
        }
    }
}
