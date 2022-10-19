using Demo.Libs.MultiTenancy.Abstractions.Infrastructure;
using FluentAssertions;
using Microsoft.Extensions.Configuration;

namespace Demo.Libs.MultiTenancy.Abstractions.Tests;

public class ConfigureMultiTenancyOptionsTests
{
    [Fact]
    public void Configure_Must_SetTenants_When_ConfigurationHaveTenants()
    {
        // prepare
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>()
            {
                { "Tenants", "A,B,C" }
                
            }).Build();

        var configureOptions = new ConfigureMultiTenancyOptions(configuration);
        var options = new MultiTenancyOptions();
        
        // act
        configureOptions.Configure(options);
        
        // assert
        var names = options.GetAllTenants();
        names.Should().HaveCount(3);
    }
}