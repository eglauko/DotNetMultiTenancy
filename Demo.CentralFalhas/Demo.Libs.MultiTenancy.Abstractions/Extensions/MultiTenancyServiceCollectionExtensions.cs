
using Demo.Libs.MultiTenancy.Abstractions;
using Demo.Libs.MultiTenancy.Abstractions.Infrastructure;
using Microsoft.Extensions.Options;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extensions methods for <see cref="IServiceCollection"/>.
/// </summary>
public static class MultiTenancyServiceCollectionExtensions
{
    /// <summary>
    /// Adiciona os serviços para suporte ao multitenancy.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns></returns>
    public static IServiceCollection AddMultiTenancy(this IServiceCollection services)
    {
        if (services.Any(sd => sd.ServiceType == typeof(TenantManager)))
            return services;
        
        services.AddScoped<TenantManager>();
        services.AddTransient<ICurrentTenant>(sp => sp.GetRequiredService<TenantManager>().CurrentTenant);
        services.AddTransient<ITenantManager>(sp => sp.GetRequiredService<TenantManager>());
        services.AddTransient<IConfigureOptions<MultiTenancyOptions>, ConfigureMultiTenancyOptions>();

        return services;
    }
}