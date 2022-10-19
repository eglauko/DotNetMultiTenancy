using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Demo.Libs.MultiTenancy.Abstractions.Infrastructure;

public class ConfigureMultiTenancyOptions : IConfigureOptions<MultiTenancyOptions>
{
    private const string TenantsKey = "Tenants";
    
    private readonly IConfiguration _configuration;

    public ConfigureMultiTenancyOptions(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(MultiTenancyOptions options)
    {
        var names = _configuration[TenantsKey];
        if (names is not null)
            options.SetTenants(names);
    }
}