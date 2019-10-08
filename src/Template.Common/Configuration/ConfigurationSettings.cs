namespace Template.Common.Configuration
{
    public class ConfigurationSettings
    {
        public GeneralConfig General { get; set; }
        public EmailConfig Email { get; set; }
        public RandomOrgConfig RandomOrg { get; set; }
        public CorsConfig Cors { get; set; }
        public SessionTokenConfig SessionToken { get; set; }
        public ConnectionStringsConfig ConnectionStrings { get; set; }
    }

    public class GeneralConfig
    {
        public string Environment { get; set; }
        public string SupportNumber { get; set; }
        public string FrontendUrl { get; set; }
        public string BackendUrl { get; set; }

        public bool IsProduction() => Environment == "Production";
        public bool IsDevelopment() => Environment == "Development";
        public bool IsLocal() => Environment == "Local";
    }

    public class EmailConfig
    {
        public string SmtpHost { get; set; }
        public string SmtpPassword { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string DisplayUsername { get; set; }
        public string SenderAddress { get; set; }
        public string SendGridKey { get; set; }
    }

    public class RandomOrgConfig
    {
        public string BaseUrl { get; set; }
        public string ApiKey { get; set; }
    }

    public class CorsConfig
    {
        public string[] Origins { get; set; }
    }

    public class SessionTokenConfig
    {
        public int LifetimeMinutes { get; set; }
        public int ByteCount { get; set; }
    }

    public class ConnectionStringsConfig
    {
        public string DefaultConnection { get; set; }
    }
}
