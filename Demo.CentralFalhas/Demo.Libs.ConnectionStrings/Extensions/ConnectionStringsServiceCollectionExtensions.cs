using Demo.Libs.ConnectionStrings;
using Demo.Libs.ConnectionStrings.Infrastructure;
using Demo.Libs.ConnectionStrings.Options;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extensions methods for <see cref="IServiceCollection"/>.
/// </summary>
public static class ConnectionStringsServiceCollectionExtensions
{
    public static IServiceCollection AddConnectionStringProvider(this IServiceCollection services)
    {
        if (services.Any(sd => sd.ServiceType == typeof(IConnectionStringProvider<>)))
            return services;
        
        services.AddScoped(typeof(IConnectionStringProvider<>), typeof(ConnectionStringProvider<>));
        services.AddScoped<ConnectionTenant>();
        services.AddSingleton<ConnectionStringResolver>();
        services.AddSingleton<OptionsMonitor>();
        services.AddTransient<IPostConfigureOptions<ConnectionStringOptions>, PostConfigureConnectionStringOptions>();
        
        return services;
    }

    public static IServiceCollection ConfigureConnectionString<TContext>(
        this IServiceCollection services,
        string databaseName)
    {
        services.Configure<ConnectionStringOptions>(typeof(TContext).Name, options => options.Database = databaseName);

        return services;
    }
}