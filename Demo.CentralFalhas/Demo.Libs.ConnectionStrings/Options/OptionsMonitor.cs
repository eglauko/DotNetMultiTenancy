using System.Collections.Concurrent;
using System.Reflection;
using Microsoft.Extensions.Options;

namespace Demo.Libs.ConnectionStrings.Options;

public class OptionsMonitor
{
    private readonly ConcurrentDictionary<string, ConnectionStringOptions> _cache = new();
    private readonly IOptionsMonitor<ConnectionStringOptions> _monitor;

    public OptionsMonitor(IOptionsMonitor<ConnectionStringOptions> monitor)
    {
        _monitor = monitor;
    }

    /// <summary>
    /// Returns a configured <see cref="ConnectionStringOptions"/> instance with the given context type.
    /// </summary>
    public ConnectionStringOptions Get<TContext>() => _cache.GetOrAdd(
        typeof(TContext).Name,
        static (name, monitor) => ValueFactory<TContext>(name, monitor),
        _monitor);

    private static ConnectionStringOptions ValueFactory<TContext>(
        string name,
        IOptionsMonitor<ConnectionStringOptions> monitor)
    {
        var options = monitor.Get(name);

        if (options.Database is not null) 
            return options;
        
        var attr = typeof(TContext).GetCustomAttribute<ConnectionNameAttribute>();
        if (attr is not null)
            options.Database = attr.Name;

        return options;
    }
}