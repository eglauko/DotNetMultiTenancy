using Demo.Libs.MultiTenancy.Abstractions;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class MultiTenancyConnectionStringsServiceCollectionExtensions
{
    public static IServiceCollection AddConnectionStringsMultiTenancySupport(this IServiceCollection services)
    {
        if (services.Any(sd => sd.ImplementationType == typeof(ConnectionTenantListener)))
            return services;

        services.AddTransient<ITenantListener, ConnectionTenantListener>();
        
        return services;
    }
}