namespace ArcaneStars.Service.Configurations
{
    public interface IServiceConfigurationAgent
    {
        string ConnectionString { get; }

        string ApiHeaderKey { get; }

        string ApiHeaderValue { get; }
        
    }
}
