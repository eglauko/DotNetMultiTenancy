using Microsoft.Extensions.Options;

namespace Demo.Libs.ConnectionStrings.Options;

internal class PostConfigureConnectionStringOptions : IPostConfigureOptions<ConnectionStringOptions>
{
    private readonly IOptions<ConnectionStringConfigurationOptions> _configurationOptions;

    public PostConfigureConnectionStringOptions(IOptions<ConnectionStringConfigurationOptions> configurationOptions)
    {
        _configurationOptions = configurationOptions;
    }

    public void PostConfigure(string name, ConnectionStringOptions options)
    {
        options.Configurations = _configurationOptions.Value;
    }
}