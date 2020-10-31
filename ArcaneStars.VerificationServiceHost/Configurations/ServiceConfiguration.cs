using ArcaneStars.Service.Configurations;
using Microsoft.Extensions.Options;

namespace ArcaneStars.ServiceHost.Configurations
{
    public class ServiceConfigurationAgent : IServiceConfigurationAgent
    {
        readonly IOptions<ConnectionStrings> _options;
        readonly IOptions<ApplicationSettings> _appSettingsOptions;
        public ServiceConfigurationAgent(IOptions<ConnectionStrings> options,
        IOptions<ApplicationSettings> appSettingsOptions)
        {
            _options = options;
            _appSettingsOptions = appSettingsOptions;
        }

        public string ConnectionString => _options.Value.ServiceDb;

        public string LogConnectionString => _options.Value.LogDb;

        public string ApiHeaderKey => _appSettingsOptions.Value.ApiKey;

        public string ApiHeaderValue => _appSettingsOptions.Value.ApiValue;
    }

    public class ConnectionStrings
    {
        public string ServiceDb { get; set; }
        
        public string LogDb { get; set; }
    }

    public class ApplicationSettings
    {
        public string ApiKey { get; set; }

        public string ApiValue { get; set; }
        
    }
}
