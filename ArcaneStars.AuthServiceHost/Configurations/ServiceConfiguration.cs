using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArcaneStars.AuthServiceHost.Configurations
{
    public interface IServiceConfigurationAgent
    {
        string ConnectionString { get; }

        string LogConnectionString { get; }

        string ApiKey { get; }

        string ApiValue { get; }

        public string UserServiceApiKey { get; }

        public string UserServiceApiValue { get; }

        public string UserServiceRootUrl { get; }

        public string VerificationServiceApiKey { get; }

        public string VerificationServiceApiValue { get; }

        public string VerificationServiceRootUrl { get; }
    }

    public class ServiceConfiguration : IServiceConfigurationAgent
    {
        readonly IOptions<ConnectionStrings> _options;
        readonly IOptions<ApplicationSettings> _appSettingsOptions;
        public ServiceConfiguration(IOptions<ConnectionStrings> options,
        IOptions<ApplicationSettings> appSettingsOptions)
        {
            _options = options;
            _appSettingsOptions = appSettingsOptions;
        }

        public string ConnectionString => _options.Value.ServiceDb;

        public string LogConnectionString => _options.Value.LogDb;

        public string ApiKey => _appSettingsOptions.Value.ApiKey;

        public string ApiValue => _appSettingsOptions.Value.ApiValue;

        public string UserServiceApiKey => _appSettingsOptions.Value.UserServiceApiKey;

        public string UserServiceApiValue => _appSettingsOptions.Value.UserServiceApiValue;

        public string UserServiceRootUrl => _appSettingsOptions.Value.UserServiceRootUrl;

        public string VerificationServiceApiKey => _appSettingsOptions.Value.VerificationServiceApiKey;

        public string VerificationServiceApiValue => _appSettingsOptions.Value.VerificationServiceApiValue;

        public string VerificationServiceRootUrl => _appSettingsOptions.Value.VerificationServiceRootUrl;
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

        public string UserServiceApiKey { get; set; }

        public string UserServiceApiValue { get; set; }

        public string UserServiceRootUrl { get; set; }

        public string VerificationServiceApiKey { get; set; }

        public string VerificationServiceApiValue { get; set; }

        public string VerificationServiceRootUrl { get; set; }
    }
}
