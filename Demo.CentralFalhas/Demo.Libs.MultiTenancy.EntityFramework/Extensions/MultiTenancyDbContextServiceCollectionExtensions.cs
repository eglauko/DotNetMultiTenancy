using System.Reflection;
using Demo.Libs.MultiTenancy.EntityFramework;
using Demo.Libs.MultiTenancy.EntityFramework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class MultiTenancyDbContextServiceCollectionExtensions
{
    public static IServiceCollection AddMultiTenancyDbContext<TContext>(
        this IServiceCollection services,
        Action<string, DbContextOptionsBuilder> configureOptions)
        where TContext : DbContext
    {
        return services.AddMultiTenancyDbContext<TContext>((_, tenant, builder) => configureOptions(tenant, builder));
    }
    
    public static IServiceCollection AddMultiTenancyDbContext<TContext>(
        this IServiceCollection services,
        Action<IServiceProvider, string, DbContextOptionsBuilder> configureOptions)
        where TContext : DbContext
    {
        CheckContextConstructors<TContext>();
        AddServicesCore(services);
            
        var options = new MultiTenancyDbContextOptions(typeof(TContext), configureOptions);
        services.AddSingleton(options);
        services.AddScoped<TContext>();
        
        return services;
    }

    public static IServiceCollection AddMultiTenancyMultiProviderMultiDatabase<TContext>(
        this IServiceCollection services)
        where TContext : DbContext
    {
        return services.AddMultiTenancyDbContext<TContext>(
            MultiTenancyMultiProviderMultiDatabaseConfigurer.ConfigureDelegate);
    }

    private static void AddServicesCore(IServiceCollection services)
    {
        if (services.Any(sd => sd.ServiceType == typeof(MultiTenancyDbContextConfiguration)))
            return;

        services.AddSingleton<MultiTenancyDbContextConfiguration>();
        services.AddTransient<IDbContextOptionsProvider, DbContextOptionsProvider>();
        
        services.AddMultiTenancy();
        services.AddConnectionStringProvider();
        services.AddConnectionStringsMultiTenancySupport();
    }
    
    private static void CheckContextConstructors<TContext>()
        where TContext : DbContext
    {
        var declaredConstructors = typeof(TContext).GetTypeInfo().DeclaredConstructors.ToList();
        if (declaredConstructors.Count != 1
            || declaredConstructors[0].GetParameters().Length != 1
            || declaredConstructors[0].GetParameters()[0].ParameterType != typeof(IDbContextOptionsProvider))
        {
            var dbName = typeof(TContext).ShortDisplayName();
            var opName = typeof(IDbContextOptionsProvider).ShortDisplayName();
            throw new ArgumentException(
                $"The DbContext of type '{dbName}' must have a single constructor with a single parameter of type '{opName}'");
        }
    }
}