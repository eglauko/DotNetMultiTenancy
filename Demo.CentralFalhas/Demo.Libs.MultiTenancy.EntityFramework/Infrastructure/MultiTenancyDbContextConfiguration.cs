using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Demo.Libs.MultiTenancy.EntityFramework.Infrastructure;

internal class MultiTenancyDbContextConfiguration
{
    private readonly InternalData _internalData;
    private readonly ConcurrentDictionary<(Type, string), DbContextOptions> _dbContextOptionsMap = new();

    public MultiTenancyDbContextConfiguration(
        IEnumerable<MultiTenancyDbContextOptions> options,
        IServiceProvider serviceProvider)
    {
        _internalData = new(new(), serviceProvider);
        foreach (var option in options)
        {
            _internalData.OptionsMap[option.DbContextType] = option;    
        }
    }

    public DbContextOptions GetDbContextOption<TContext>(string? tenantName) 
        where TContext : DbContext
    {
        return _dbContextOptionsMap.GetOrAdd((typeof(TContext), tenantName), static (key, data) =>
        {
            if (data.OptionsMap.TryGetValue(key.Item1, out var options))
            {
                var builder = new DbContextOptionsBuilder<TContext>(
                    new DbContextOptions<TContext>(new Dictionary<Type, IDbContextOptionsExtension>()));

                builder.UseApplicationServiceProvider(data.ServiceProvider);

                options.ConfigureOptions(data.ServiceProvider, key.Item2, builder);

                return builder.Options;
            }

            throw new InvalidOperationException("The context type '{}' is not configured");
            
        }, _internalData);
    }

    private record InternalData(
        Dictionary<Type, MultiTenancyDbContextOptions> OptionsMap,
        IServiceProvider ServiceProvider);
}